CREATE function [dbo].[cleanString] (@strIn as varchar(8000))
returns varchar(8000)
as
begin
 declare @iPtr as int
 set @iPtr = patindex('%[^ -~0-9A-Z]%', @strIn COLLATE LATIN1_GENERAL_BIN)
 while @iPtr > 0 begin
  set @strIn = replace(@strIn COLLATE LATIN1_GENERAL_BIN, substring(@strIn, @iPtr, 1), '')
  set @iPtr = patindex('%[^ -~0-9A-Z]%', @strIn COLLATE LATIN1_GENERAL_BIN)
 end
 return @strIn
end
