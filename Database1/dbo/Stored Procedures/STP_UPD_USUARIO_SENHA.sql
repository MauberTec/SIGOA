CREATE procedure [dbo].[STP_UPD_USUARIO_SENHA]
@usu_id int, 
@pwd_senhacrip nvarchar(240), 
@usu_id_Atualizacao bigint

--with encryption
as
begin try
		BEGIN TRAN T1
		declare @actionDate datetime
		set @actionDate = getdate()
		
		declare @tem int = 0;
		declare @aux varchar(max)='';


		set @aux = ( 
					 select top 1 case when sen_senha_id is null then '0' else sen_senha_id end
					 from dbo.tab_senhas 
					 where usu_id = @usu_id
					       and sen_ativo = 0	
					 order by sen_data_criacao desc
					);

		if (@aux = @pwd_senhacrip)
		  set @tem =1;
		else
		  set @tem =0;

		
		if (@tem = 0)
				begin					
						-- inativa o existente
						update dbo.tab_senhas
							set sen_ativo = 0,
								sen_mudar_senha = 0,
								sen_atualizado_por = @usu_id_Atualizacao,
								sen_data_atualizacao= @actionDate 
							where usu_id = @usu_id 
							and sen_ativo = 1;

						-- cria um ativo novo
						insert into dbo.tab_senhas (usu_id, sen_senha_id, sen_ativo, sen_mudar_senha, sen_data_criacao, sen_criado_por)
						values (@usu_id,@pwd_senhacrip ,1, 0, @actionDate, @usu_id_Atualizacao);

						select 1 as retorno;
				end
				else
				begin
					--'Esta senha já foi usada neste sistema.'
					 select -10 as retorno;
				end;

		COMMIT TRAN T1;
end try
begin catch
		ROLLBACK TRAN T1
           -- PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()
			select -1 as retorno;

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
