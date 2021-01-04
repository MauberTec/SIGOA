CREATE procedure[dbo].[STP_LOGIN] (@usu_usuario varchar (20), 
								 @senhacrip varchar (500), 
								 @ip varchar(500))
as
begin

	declare @usu_id int = -1;
	declare @usu_ativo bit = 0;
	declare @usu_nome varchar (100)
	declare @usu_foto varchar (max)
	declare @tra_id [smallint] = 1;
	declare @mod_id [int] = -1 ; 
	declare @log_texto [varchar](max) = ' ';
	declare @mudarSenha bit = 0;
	
	SELECT @usu_id = usu.usu_id, 
			@usu_ativo = usu_ativo,
			@usu_nome = usu_nome, 
			@usu_foto = usu_foto
	from  dbo.tab_usuarios usu
		inner join dbo.tab_senhas pwd on pwd.usu_id = usu.usu_id and sen_ativo=1 --and usu_ativo=1
	where UPPER(usu_usuario) = @usu_usuario 
			and  sen_senha_id= @senhacrip;
			

	if ((@usu_id IS NULL) or (@usu_id = -1) )
		begin
			SET @usu_id = '-1'--'Erro: Usuario/Senha Invalidos.'
			set @usu_nome = null;	
			
			--Falha ao acessar o sistema
			set @tra_id = 7;
			exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_usuario, @mod_id, @log_texto, @ip			
		end
	else
		begin
			--'Sucesso ao acessar sistema
			exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id, @log_texto, @ip			

			set @mudarSenha = ( select top 1  isnull(sen_mudar_senha,0) as sen_mudar_senha
									 from dbo.tab_senhas 
									 where usu_id = @usu_id
											 and sen_ativo=1);
		end

	select  @usu_id as usu_id, 
			@usu_ativo as usu_ativo,
			isnull(@usu_foto,'') as usu_foto,
			isnull(@usu_nome,'') as usu_nome,
			@mudarSenha as sen_mudar_senha
end
