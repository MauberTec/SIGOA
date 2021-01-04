CREATE function [dbo].[formatN](@number varchar(30) ) returns varchar(50)
As
Begin

    -- add thousands seperator for every 3 digits to the left of the decimal place
	declare @result varchar(50) 
	set @result = @number 

	declare @pos int
    set @pos = CHARINDEX(',', @result)

    while @pos > 4
    begin
        set @result = STUFF(@result, @pos-3, 0, '.')
        set @pos = CHARINDEX('.', @result)
    end


    return @result
end
