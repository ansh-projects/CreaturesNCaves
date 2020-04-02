-- Table: public.campaigns

-- DROP TABLE public.campaigns;

CREATE TABLE public.campaigns
(
    id text COLLATE pg_catalog."default" NOT NULL,
    name text COLLATE pg_catalog."default",
    user_id text COLLATE pg_catalog."default" NOT NULL,
    description text COLLATE pg_catalog."default",
    CONSTRAINT campaigns_pkey PRIMARY KEY (id),
    CONSTRAINT "campaigns_AspNetRoles_user_id_fkey" FOREIGN KEY (user_id)
        REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.campaigns
    OWNER to cnc_admin;