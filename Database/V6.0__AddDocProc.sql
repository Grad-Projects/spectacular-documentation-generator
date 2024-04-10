CREATE OR REPLACE PROCEDURE add_doc(style_id INT, doc_name TEXT, doc_content TEXT, user_id INT)
LANGUAGE plpgsql
AS $$
BEGIN
   INSERT INTO public.documents("styleID","documentName", "documentContent","userID")
    VALUES (style_id, doc_name, doc_content, user_id);
   
   COMMIT;
END;
$$;