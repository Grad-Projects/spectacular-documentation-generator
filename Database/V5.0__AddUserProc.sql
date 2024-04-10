CREATE OR REPLACE PROCEDURE add_user(user_name TEXT)
LANGUAGE plpgsql
AS $$
BEGIN
   INSERT INTO public.users("githubUserName")
    VALUES (user_name);
   
   COMMIT;
END;
$$;