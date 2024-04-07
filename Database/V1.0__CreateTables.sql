CREATE TABLE public.styles
(
    "styleID" serial NOT NULL,
    "styleName" text NOT NULL,
    "styleContent" text,
    PRIMARY KEY ("styleID")
);

CREATE TABLE public.documents
(
    "documentID" serial NOT NULL,
    "styleID" integer,
    "documentName" text,
    "documentContent" text,
    CONSTRAINT "documents_PK" PRIMARY KEY ("documentID"),
    CONSTRAINT "style_FK" FOREIGN KEY ("styleID")
        REFERENCES public.styles ("styleID") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
);

CREATE TABLE public."generationHistory"
(
    "generationID" serial NOT NULL,
    "documentID" integer,
    "generatedTimeStamp" timestamp with time zone,
    "documentVersion" integer,
    CONSTRAINT "generationHistory_PK" PRIMARY KEY ("generationID"),
    CONSTRAINT "documents_FK" FOREIGN KEY ("documentID")
        REFERENCES public.documents ("documentID") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
);




