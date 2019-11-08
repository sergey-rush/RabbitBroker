SELECT * FROM public.users;
--insert into users(user_name) values ('Fatal')

SELECT * FROM incomes;
--update users set amount = 0;
--update incomes set amount = 500000;
--delete from accounts where user_id = 1 and amount > 0
--insert into incomes (user_id, amount) values(5, 5000)
SELECT * FROM accounts order by account_id desc;

--insert into accounts (user_id, amount) values(5, 0)
select count(*) as Totals from accounts;

--ALTER TABLE public.users ALTER column amount NOT NULL;
--
--CREATE TABLE public.accounts (
--	account_id int4 NOT NULL,
--	user_id int4 NOT NULL,
--	amount numeric NOT NULL
--);
--
--CREATE SEQUENCE public.accounts_id_seq
--	INCREMENT BY 1
--	MINVALUE 1
--	MAXVALUE 9223372036854775807
--	START 1
--	CACHE 1
--	NO CYCLE;
--
---- Permissions
--
--ALTER SEQUENCE public.accounts_id_seq OWNER TO postgres;
--GRANT ALL ON SEQUENCE public.accounts_id_seq TO postgres;

--nextval('"incomes_id_seq"'::text::regclass)

CREATE OR REPLACE FUNCTION public.process_fee(p_user_id integer, p_fee numeric)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
declare
p_income_row incomes%ROWTYPE;
p_account_row accounts%ROWTYPE;
	BEGIN
	SELECT * FROM incomes WHERE user_id = p_user_id INTO p_income_row;
	update incomes set amount = (amount - p_fee) WHERE user_id = p_user_id;
	SELECT * FROM accounts WHERE user_id = p_user_id INTO p_account_row order by account_id desc limit 1;
	insert into accounts (user_id, amount) values (p_user_id, p_account_row.amount + p_fee);
	update users set amount = p_account_row.amount + p_fee WHERE user_id = p_user_id;
end;

$function$
;
