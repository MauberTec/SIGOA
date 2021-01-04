create FUNCTION [dbo].[checaValueInList] (@testValue int, @list nvarchar(MAX))
   RETURNS int
   AS

BEGIN
   declare @saida int=0;
   declare @valor int=0;

   declare @pos        int;
   declare @nextpos    int;
   declare @valuelen   int;

   set @pos = 0;
   set @nextpos = 1;

   while (@nextpos > 0 and @saida = 0)
   begin
      set @nextpos = charindex(',', @list, @pos + 1);
      set @valuelen = case when @nextpos > 0
                              then @nextpos
                              else len(@list) + 1
                      end - @pos - 1;

      set @valor = convert(int, substring(@list, @pos + 1, @valuelen));

	  if (@valor = @testValue)
	    set @saida = 1;

      set @pos = @nextpos;
   end
   
   return @saida;

END
