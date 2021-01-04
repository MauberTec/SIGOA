CREATE procedure [dbo].[STP_COMPARA_COLS_ANTES_DEPOIS]
@tabela varchar(300),
@retorno VARCHAR(MAX) output

AS
BEGIN

	set nocount on;

	-- checa se as tmp tables existem e as exclui
	IF OBJECT_ID('tempdb..##temp') IS NOT NULL
	DROP TABLE ##temp;

	IF OBJECT_ID('tempdb..##temp2') IS NOT NULL
	   DROP TABLE ##temp2;

	-- faz a transposicao de linha para coluna
	create table [dbo].[##temp2]( [NomeColuna] [nvarchar](200),
								  [1] [nvarchar](max) null,
								  [2] [nvarchar](max) null);

	declare @Columnname varchar(100) 
	declare @vUm varchar(max);
	declare @vDois varchar(max);

	declare col_cursor cursor  
				for select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = @tabela;

    open col_cursor;
    fetch next from col_cursor into @Columnname 
    while @@FETCH_STATUS = 0
	  begin
			--select @Columnname, @ColumnIndex ;
			set @vUm = 'insert into [##temp2] (NomeColuna, [1],[2]) select' + char(39) + @Columnname + char(39) + ',' + @Columnname + ', NULL from #tmpComparar where nRow = 1';
			exec (@vUm);

			set @vDois = 'update [##temp2] set [2] = (select ' +  @Columnname + ' from #tmpComparar where nRow = 2) WHERE NomeColuna = '+ char(39) + @Columnname + char(39);
			exec (@vDois);

			fetch next from col_cursor into @Columnname;
	  end;
	close col_cursor;
	DEALLOCATE col_cursor;

	delete from [##temp2]
	where 	NomeColuna  like '%_deletado' 
			or NomeColuna  like '%_data_criacao' 
			or NomeColuna  like '%_criado_por' 
			or NomeColuna  like '%_data_atualizacao' 
			or NomeColuna  like '%_atualizado_por';

	-- *********************************************************
	-- concatena a saida
	-- *********************************************************

	-- encontra e concatena as chaves primarias
	  declare @PKs varchar(MAX); 
	  select @PKs = COALESCE(@PKs + '; ' + 'Chave [' +  NomeColuna + ']= [' + [2] + ']', 'Chave [' +  NomeColuna + ']= [' + [2] + ']') 
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
	  Select @logs = COALESCE(@logs + '; ' + 'Coluna [' +  NomeColuna + '] de [' + [1] + '] para [' + [2] + ']', 'Coluna [' +  NomeColuna + '] de [' + [1] + '] para [' + [2] + ']') 
		From ##temp2
		where [1] <> [2]
		and [NomeColuna] not in ( SELECT Col.Column_Name from -- chaves da tabela
										INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab, 
										INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col 
									WHERE 
										Col.Constraint_Name = Tab.Constraint_Name
										AND Col.Table_Name = Tab.Table_Name
										AND Constraint_Type = 'PRIMARY KEY'
										AND Col.Table_Name = @tabela
								);

-- exclui as temporarias
DROP TABLE ##temp2;


-- retorno dos valores concatenados:
if @PKs is null 
	set @PKs = '';

if @logs is null 
	set @retorno = '';
else
	set @retorno = @PKs + '; ' + @logs

--retorna 
	--select @retorno -- não é necessario esse select

set nocount off;

END
