CREATE procedure [dbo].[STP_UPD_RESETA_SENHA] 
@usu_usuario nvarchar(20),
@pwd_senhacrip nvarchar(max)

--with encryption
as

begin try
		BEGIN TRAN T1
				declare @actionDate datetime
				set @actionDate = getdate()

			-- checa se usuario existe
			declare @usu_id int ;
			set @usu_id = (select isnull(usu_id,-1) 
									from dbo.tab_usuarios
									where usu_usuario = @usu_usuario
										and usu_ativo=1
										and usu_deletado is null);

			if (@usu_id >=0)	
			begin		
				-- desativa a senha antiga
				update dbo.tab_senhas
				    set sen_ativo = 0,
						sen_mudar_senha = 0,
						sen_data_atualizacao= @actionDate,
						sen_atualizado_por = 0
					where usu_id = @usu_id
							and sen_ativo = 1;
				
				-- coloca senha nova
				insert into dbo.tab_senhas (usu_id, sen_senha_id, sen_ativo, sen_mudar_senha, sen_data_criacao, sen_criado_por)
				values (@usu_id,@pwd_senhacrip ,1,1, @actionDate, 0);
				
				
				-- retorna os dados do usuario
				 select  top 1 * 
				  FROM  dbo.tab_usuarios
				  where usu_deletado is  null
					  and usu_ativo = 1
					  and usu_id = @usu_id; 

			end
			else
				return null;
		COMMIT TRAN T1

		
end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
		return 0
end catch
