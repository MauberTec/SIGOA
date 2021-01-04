CREATE function [dbo].[dec2hex] (@numDec bigint)
returns varchar(6)
as
begin
		--declare @numDec bigint = convert(bigint, @snumDec) ;

		declare @snumDec varchar(15) = convert(nvarchar(6), @numDec);
		declare @numHex varchar(6) = '';
		declare @numTmp varchar(6);
		declare @pedaco bigint = 0;
		declare @i int = len(@snumDec);

		while @numDec > 0
		begin
			set @pedaco = (@numDec % 16); -- resto da divisao 

			if  (@pedaco >= 10)
				set @numTmp = (select char(@pedaco + 55));
			else
				set @numTmp = (select char(@pedaco + 48));

			set @numHex = @numTmp + @numHex ;
			set @numDec =  @numDec / 16;
		end
		 
		return right('000000' + @numHex, 6);
end
