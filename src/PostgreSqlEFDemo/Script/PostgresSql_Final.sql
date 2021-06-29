--DROP FUNCTION get_queue(integer)


CREATE OR REPLACE FUNCTION public.get_queue(limitCount integer)
    RETURNS TABLE(p_accountid integer, p_amount integer) 
    LANGUAGE 'plpgsql'
AS $BODY$

DECLARE _accountId int;
DECLARE _amount integer;

BEGIN
   FOR _accountId,_amount IN
             SELECT accountid, amount
                FROM Account
               WHERE 
				processed_on IS NULL
				ORDER BY accountid
               LIMIT limitCount FOR UPDATE SKIP LOCKED
   LOOP
      UPDATE Account SET processed_on = NOW()
         WHERE accountid = _accountId;
		
	  p_accountid := _accountId;
	  p_amount := _amount;
	  
      RETURN NEXT;
   END LOOP;
   RETURN;
END;
$BODY$;