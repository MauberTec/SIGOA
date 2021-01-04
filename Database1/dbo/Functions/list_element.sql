CREATE function [dbo].[list_element] ( @p_string VARCHAR(max), @p_element int, @p_separator VARCHAR(max) ) RETURNS nvarchar(100)
AS
BEGIN

declare @v_string  VARCHAR(max)  
declare @valor  VARCHAR(max)  
   set @v_string = @p_string + @p_separator  

  
   declare @i int
   declare @idx int
   declare @idxAnt int
   
   set @idxAnt =0
   set @idx = 0
   set @i = 1   
   
   while @i <= @p_element
   begin
		set @idxAnt = @idx
		set @idx = CHARINDEX (@p_separator, @v_string, @idx + 1)
		set @i = @i + 1
   end

   if (@idx >0)
      set @valor = substring(@v_string, @idxAnt + 1, @idx - @idxAnt-1 )
   else 
      set @valor = ''

   return @valor

END
