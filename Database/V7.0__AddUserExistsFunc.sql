CREATE FUNCTION check_user_exists(user_name TEXT) RETURNS boolean
    AS 'SELECT EXISTS(SELECT 1 FROM users WHERE "githubUserName"=user_name);'
    LANGUAGE SQL
    IMMUTABLE
    RETURNS NULL ON NULL INPUT;