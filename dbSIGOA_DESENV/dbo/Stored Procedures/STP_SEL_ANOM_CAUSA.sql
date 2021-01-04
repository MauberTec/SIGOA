CREATE PROCEDURE [dbo].[STP_SEL_ANOM_CAUSA] 
@aca_id int=null,
@leg_id int=null,
@aca_descricao nVARCHAR(255) = '' 

AS 
  BEGIN 

	  if (@aca_descricao <> '') and (@leg_id is not null)
		  SELECT  * 
		  FROM  dbo.tab_anomalia_causas ACA
	  		  inner join [dbo].[tab_anomalia_legendas] leg on leg.leg_id = aca.leg_id and leg.leg_deletado is null
		  where aca_deletado is  null
		    and aca_descricao = @aca_descricao
			and aca.leg_id = @leg_id;
	   else
	      if (@aca_id is not null)
			  SELECT  * 
			  FROM  dbo.tab_anomalia_causas ACA
	  			  inner join [dbo].[tab_anomalia_legendas] leg on leg.leg_id = aca.leg_id and leg.leg_deletado is null
			  where aca_deletado is  null
				and aca_id = @aca_id ;
			else
			  SELECT  * 
			  FROM  dbo.tab_anomalia_causas ACA
	  			  inner join [dbo].[tab_anomalia_legendas] leg on leg.leg_id = aca.leg_id and leg.leg_deletado is null
			  where aca_deletado is  null;

  END ;
