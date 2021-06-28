create function get_row_count()
returns int
language plpgsql
AS 
$$
declare
   total_count integer;
begin
   select count(*) 
        total_count
   from Account
   return total_count;
end;
$$;