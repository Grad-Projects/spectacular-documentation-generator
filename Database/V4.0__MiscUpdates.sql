DROP TABLE "generationHistory";

CREATE TABLE public.users
(
    "userID" serial NOT NULL,
    "githubUserName" text,
    CONSTRAINT "users_PK" PRIMARY KEY ("userID")
);

ALTER TABLE "documents"
ADD "userID" int;

ALTER TABLE "documents"
    ADD CONSTRAINT fk_documents_users FOREIGN KEY ("userID") REFERENCES "users" ("userID");