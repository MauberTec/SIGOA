CREATE function [dbo].[fn_TableLevel] (@tablename varchar(300)) returns varchar (max)
as
begin 
	declare @nivel varchar(max);
	
with Fkeys as ( 
				select distinct  OnTable = OnTable.name,
								 AgainstTable  = AgainstTable.name 
				from sysforeignkeys fk
					inner join sysobjects onTable on fk.fkeyid = onTable.id
					inner join sysobjects againstTable on fk.rkeyid = againstTable.id
				where 1=1
					AND AgainstTable.TYPE = 'U'
					AND OnTable.TYPE = 'U'
					-- ignore self joins; they cause an infinite recursion
					and OnTable.Name <> AgainstTable.Name
				)
,MyData as (
				select 
						OnTable = o.name
					,AgainstTable = FKeys.againstTable
				from sys.objects o
					left join FKeys on  o.name = FKeys.onTable
				where 1=1 and o.type = 'U'
					      and o.name not like 'sys%'
								)
,MyRecursion as (
				-- base case
				select 
					 TableName    = OnTable
					,Lvl        = 1
					,DepPath    = convert(varchar(max), OnTable)
				from  MyData
				where 1=1 and AgainstTable is null

				-- recursive case
				union all select
					 TableName    = OnTable
					,Lvl        = r.Lvl + 1
					,DepPath    = convert(varchar(max), r.DepPath + '|' + OnTable)
				from MyData d inner join MyRecursion r  on d.AgainstTable = r.TableName
				)

	--select convert(varchar(3), Lvl) as Level , DepPath
	--from  MyRecursion
	--where TableName = @tablename 
	--order by DepPath

--	 Level = replicate('..', Lvl) + convert(varchar(3), Lvl)
select @nivel= (convert(varchar(3), Lvl) + ':'+ DepPath)				
					from  MyRecursion					
					 where TableName = @tablename 
	
	return @nivel
end
