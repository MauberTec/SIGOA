CREATE procedure [dbo].[STP_CONCATENA_COLS_VALS]
@tabela varchar(300),
@retorno VARCHAR(MAX) output,
@tudo int = null

AS
BEGIN
set nocount on;

	-- checa se as tmp tables existem e as exclui
	IF OBJECT_ID('tempdb..##temp') IS NOT NULL
	DROP TABLE ##temp;

	IF OBJECT_ID('tempdb..##temp2') IS NOT NULL
	DROP TABLE ##temp2;

	-- faz a transposicao de linha para coluna
    DECLARE @Xmldata XML = (SELECT * FROM #tmpTabela FOR XML PATH('')) ;
      
    --Dynamic unpivoting
    SELECT * INTO ##temp 
	FROM ( SELECT  ROW_NUMBER() OVER(PARTITION BY NomeColuna ORDER BY Valor) as nLinha,* 
			FROM ( SELECT  i.value('local-name(.)','varchar(100)') as NomeColuna,
						   isnull(i.value('.','varchar(100)'),'') as Valor
					FROM @xmldata.nodes('//*[text()]') x(i) 
				 ) as tmp 
		 ) as tmp1;
      
    --Dynamic pivoting
    DECLARE @Columns NVARCHAR(MAX),@query NVARCHAR(MAX);
    SELECT @Columns = STUFF((SELECT  ', ' +QUOTENAME(CONVERT(VARCHAR,nLinha)) 
							 FROM (SELECT DISTINCT nLinha FROM ##temp ) AS T FOR XML PATH('')), 1,2,''); 
	
	CREATE TABLE [dbo].[##temp2]( [NomeColuna] [nvarchar](200),
								  [1] [nvarchar](max)) ;



    SET @query = N' insert into ##temp2 (NomeColuna, [1])
					SELECT NomeColuna,' + @Columns + '					
					FROM ( SELECT * FROM ##temp )  i
					PIVOT (  MAX(Valor) FOR nLinha IN (' + @Columns + ') )  j ';
  if (@tudo <> 1)
	 SET @query = @query + N' where 
							   [NomeColuna] not like ' + char(39) + '%_deletado' + char(39) + 
							 ' and [NomeColuna] not like ' + char(39) + '%_data_criacao' + char(39) + 
							 ' and [NomeColuna] not like ' + char(39) + '%_criado_por' + char(39) + 
							 ' and [NomeColuna] not like ' + char(39) + '%_data_atualizacao' + char(39) + 
							 ' and [NomeColuna] not like ' + char(39) + '%_atualizado_por' + char(39) ;

    EXEC (@query);	

	-- *********************************************************
	-- concatena a saida
	-- *********************************************************

	-- encontra e concatena as chaves primarias
	declare @PKs varchar(MAX); 
	  select @PKs = COALESCE(@PKs + '; Chave [' +  NomeColuna + ']= [' + [1] + ']', 'Chave [' +  NomeColuna + ']= [' + [1] + ']') 
		From ##temp2
		where [NomeColuna] in ( SELECT Col.Column_Name from -- chaves da tabela
										INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab, 
										INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col 
									WHERE 
										Col.Constraint_Name = Tab.Constraint_Name
										AND Col.Table_Name = Tab.Table_Name
										AND Constraint_Type = 'PRIMARY KEY'
										AND Col.Table_Name = @tabela
								);

	  -- concatena as outras colunas
	  declare @logs varchar(MAX);
	  Select @logs = COALESCE(@logs + '; ' + 'Coluna [' +  NomeColuna + ']= [' + [1] + ']', 'Coluna [' +  NomeColuna + ']= [' + [1] + ']') 
		From ##temp2
		where [NomeColuna] not in ( SELECT Col.Column_Name from -- chaves da tabela
										INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab, 
										INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col 
									WHERE 
										Col.Constraint_Name = Tab.Constraint_Name
										AND Col.Table_Name = Tab.Table_Name
										AND Constraint_Type = 'PRIMARY KEY'
										AND Col.Table_Name = @tabela
								);

-- exclui as temporarias
DROP TABLE ##temp;
DROP TABLE ##temp2;

-- retorno dos valores concatenados:
if @PKs is null 
	set @PKs = '';

if @logs is null 
	select @retorno = '';
else
	select @retorno = @PKs + '; ' + @logs


set nocount off;

END
