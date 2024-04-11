CREATE FUNCTION check_style_exists(style_name TEXT) RETURNS boolean
    AS 'SELECT EXISTS(SELECT 1 FROM styles WHERE "styleName"=style_name);'
    LANGUAGE SQL
    IMMUTABLE
    RETURNS NULL ON NULL INPUT;