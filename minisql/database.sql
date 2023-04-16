CREATE TABLE "public"."dwr_person" ( 
  "id" SERIAL,
  "name" VARCHAR(255) NOT NULL,
  CONSTRAINT "dwr_person_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "public"."dwr_project" ( 
  "id" SERIAL,
  "project_name" VARCHAR(255) NOT NULL,
  CONSTRAINT "dwr_project_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "public"."dwr_project_person" ( 
  "id" SERIAL,
  "project_id" INTEGER NULL,
  "person_id" INTEGER NULL,
  "hours_worked" INTEGER NOT NULL DEFAULT 0 ,
  CONSTRAINT "dwr_project_person_pkey" PRIMARY KEY ("id")
);
ALTER TABLE "public"."dwr_project_person" ADD CONSTRAINT "dwr_project_person_project_id_fkey" FOREIGN KEY ("project_id") REFERENCES "public"."dwr_project" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."dwr_project_person" ADD CONSTRAINT "dwr_project_person_person_id_fkey" FOREIGN KEY ("person_id") REFERENCES "public"."dwr_person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;

INSERT INTO "public"."dwr_project" ("id", "project_name") VALUES (1, 'Web App');
INSERT INTO "public"."dwr_project" ("id", "project_name") VALUES (2, 'Game');
INSERT INTO "public"."dwr_project" ("id", "project_name") VALUES (3, 'App');
INSERT INTO "public"."dwr_project" ("id", "project_name") VALUES (4, 'Instagram');
INSERT INTO "public"."dwr_project" ("id", "project_name") VALUES (5, 'Facebook');
INSERT INTO "public"."dwr_project" ("id", "project_name") VALUES (6, 'LinkedIn 2');

INSERT INTO "public"."dwr_person" ("id", "name") VALUES (1, 'Alice');
INSERT INTO "public"."dwr_person" ("id", "name") VALUES (2, 'Bob');
INSERT INTO "public"."dwr_person" ("id", "name") VALUES (3, 'Charlie');
INSERT INTO "public"."dwr_person" ("id", "name") VALUES (4, 'David');
INSERT INTO "public"."dwr_person" ("id", "name") VALUES (10, 'Niko');
INSERT INTO "public"."dwr_person" ("id", "name") VALUES (13, 'Leo');

INSERT INTO "public"."dwr_project_person" ("project_id", "person_id") VALUES (1, 1);
INSERT INTO "public"."dwr_project_person" ("project_id", "person_id") VALUES (1, 10);
INSERT INTO "public"."dwr_project_person" ("project_id", "person_id") VALUES (2, 4);
INSERT INTO "public"."dwr_project_person" ("project_id", "person_id") VALUES (2, 2);
INSERT INTO "public"."dwr_project_person" ("project_id", "person_id") VALUES (3, 4);
INSERT INTO "public"."dwr_project_person" ("project_id", "person_id") VALUES (4, 10);
