CREATE procedure [dbo].[STP_SEL_ORDEM_SERVICO_PROXIMO_CODIGO] 
	@tos_id int 
as

begin try

  	declare @ord_sequencial_tipo bigint = isnull( (select max(ord_sequencial_tipo) from  dbo.tab_ordens_servico where tos_id = @tos_id ),0)+1 ;
	declare @ord_codigo varchar(20) = (select tos_codigo from dbo.tab_ordem_servico_tipos where tos_id = @tos_id) + '-' + right('000000' + convert(varchar(6), @ord_sequencial_tipo),6);

	select @ord_codigo as ord_codigo;

end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
