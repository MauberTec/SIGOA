CREATE function [dbo].[ReplaceApostrofos] (@strIn as varchar(max))
returns varchar(max)
as
begin

set @strIn = REPLACE(@strIn,char(39),char(39)+' + char(39) + '+ char(39)) 
--	set @strIn = REPLACE(@strIn,char(39),' + char(39) + ') 
--		set @strIn = REPLACE(@strIn,'''','''') 
 return @strIn
end
