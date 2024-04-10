CREATE OR REPLACE PROCEDURE add_doc(style_id INT, doc_name TEXT, doc_content TEXT, user_name TEXT)
LANGUAGE plpgsql
AS $$
declare 
user_id int;
BEGIN
   SELECT 1 FROM public.users WHERE "githubUserName" = user_name INTO user_id;
   INSERT INTO public.documents("styleID","documentName", "documentContent","userID")
    VALUES (style_id, doc_name, doc_content, user_id);
   
   COMMIT;
END;
$$;