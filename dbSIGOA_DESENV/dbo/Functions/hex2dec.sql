CREATE function [dbo].[hex2dec] (@numHex varchar(6))
returns bigint
as
begin
		declare @numDec bigint = 0;

		set @numHex = UPPER(@numHex);
		declare @numTmp bigint = 0;
		declare @pedaco varchar(1)='';
		declare @i int = len(@numHex);

		while @i > 0
		begin
			set @pedaco = SUBSTRING(@numHex,@i,1);
			set @numTmp = ASCII(@pedaco);

			if (@numTmp <= 57)
			  set @numTmp = @numTmp - 48;
			else
			  set @numTmp = @numTmp - 55;

			set @numDec =  POWER(16, len(@numHex) - @i)*@numTmp  + @numDec;
			set @i = @i -1;
		end

		return @numDec;
end
