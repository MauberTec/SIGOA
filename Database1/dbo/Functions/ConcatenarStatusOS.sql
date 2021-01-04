create function [dbo].[ConcatenarStatusOS](@sos_id_inicial int) returns varchar(max)
as
begin

declare @saida varchar(max);

select  @saida = COALESCE(@saida + ';', '') + RIGHT('000'+CAST(sos_id AS VARCHAR(3)),3)+ ':' + rtrim(sos_descricao)
  FROM dbo.tab_ordem_servico_fluxos_status fos
  inner join dbo.tab_ordem_servico_status sos on sos.sos_id = fos.sos_id_para and sos.sos_ativo = 1 and sos.sos_deletado is null
  where 
	  fos.fos_ativo = 1 
	  and fos.fos_deletado is null
	  and sos_id_de = @sos_id_inicial
  order by sos_descricao
  
declare @saida2 varchar(max);
set @saida2 = (select  RIGHT('000'+ CAST(sos_id AS VARCHAR(3)),3)+ ':' +  rtrim(sos_descricao)
				from dbo.tab_ordem_servico_status sos 
				where sos.sos_ativo = 1 and sos.sos_deletado is null
				     and sos_id = @sos_id_inicial) ;

set @saida = @saida2 + ';' + @saida;

 return isnull(@saida,'')

end
