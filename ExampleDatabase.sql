PGDMP                         x            ExampleDatabase    12.4    12.4                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    21015    ExampleDatabase    DATABASE     �   CREATE DATABASE "ExampleDatabase" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_United States.1252' LC_CTYPE = 'English_United States.1252';
 !   DROP DATABASE "ExampleDatabase";
                postgres    false            �            1255    21154 1   create_card(character varying, character varying)    FUNCTION     .  CREATE FUNCTION public.create_card(name character varying, description character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
	DECLARE
		id integer;
	BEGIN
		INSERT INTO "public"."Cards" ("Name", "Description")
		VALUES (name, description)
		RETURNING "Id" INTO id;
	
		RETURN id;
	END;
$$;
 Y   DROP FUNCTION public.create_card(name character varying, description character varying);
       public          postgres    false            �            1255    21156    delete_card_by_id(integer)    FUNCTION        CREATE FUNCTION public.delete_card_by_id(id integer) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
	BEGIN
		DELETE FROM "public"."Cards" c
		WHERE c."Id" = id;
		
		IF FOUND THEN
      		RETURN TRUE;
   		ELSE
      		RETURN FALSE;
   		END IF;
	END;
$$;
 4   DROP FUNCTION public.delete_card_by_id(id integer);
       public          postgres    false            �            1259    21034    Cards    TABLE     �   CREATE TABLE public."Cards" (
    "Id" integer NOT NULL,
    "Name" character varying(128) NOT NULL,
    "Description" character varying(256)
);
    DROP TABLE public."Cards";
       public         heap    postgres    false            �            1255    21110    get_card_by_id(integer)    FUNCTION     �   CREATE FUNCTION public.get_card_by_id(id integer) RETURNS SETOF public."Cards"
    LANGUAGE plpgsql
    AS $$
	BEGIN
		RETURN QUERY
		SELECT c."Id", c."Name", c."Description"
		FROM "public"."Cards" c
		WHERE c."Id" = id;
	END;
$$;
 1   DROP FUNCTION public.get_card_by_id(id integer);
       public          postgres    false    205            �            1255    21162 #   get_card_by_name(character varying)    FUNCTION     �   CREATE FUNCTION public.get_card_by_name(name character varying) RETURNS SETOF public."Cards"
    LANGUAGE plpgsql
    AS $$
BEGIN
		RETURN QUERY
		SELECT c."Id", c."Name", c."Description"
		FROM "public"."Cards" c
		WHERE c."Name" = name;
	END;
$$;
 ?   DROP FUNCTION public.get_card_by_name(name character varying);
       public          postgres    false    205            �            1255    21155 :   update_card(integer, character varying, character varying)    FUNCTION     \  CREATE FUNCTION public.update_card(id integer, name character varying, description character varying) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
	BEGIN
		UPDATE "public"."Cards"
		SET "Name" = name,
			"Description" = description
		WHERE "Id" = id;
		
		IF FOUND THEN
      		RETURN TRUE;
   		ELSE
      		RETURN FALSE;
   		END IF;
	END;
$$;
 e   DROP FUNCTION public.update_card(id integer, name character varying, description character varying);
       public          postgres    false            �            1259    21032    Cards_Id_seq    SEQUENCE     �   CREATE SEQUENCE public."Cards_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public."Cards_Id_seq";
       public          postgres    false    205                       0    0    Cards_Id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public."Cards_Id_seq" OWNED BY public."Cards"."Id";
          public          postgres    false    204            �            1259    21026    Comments    TABLE     �   CREATE TABLE public."Comments" (
    "Id" integer NOT NULL,
    "Value" character varying(256) NOT NULL,
    "CardId" integer NOT NULL
);
    DROP TABLE public."Comments";
       public         heap    postgres    false            �            1259    21024    Comments_Id_seq    SEQUENCE     �   CREATE SEQUENCE public."Comments_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public."Comments_Id_seq";
       public          postgres    false    203                       0    0    Comments_Id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public."Comments_Id_seq" OWNED BY public."Comments"."Id";
          public          postgres    false    202            �
           2604    21037    Cards Id    DEFAULT     j   ALTER TABLE ONLY public."Cards" ALTER COLUMN "Id" SET DEFAULT nextval('public."Cards_Id_seq"'::regclass);
 ;   ALTER TABLE public."Cards" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    204    205    205            �
           2604    21029    Comments Id    DEFAULT     p   ALTER TABLE ONLY public."Comments" ALTER COLUMN "Id" SET DEFAULT nextval('public."Comments_Id_seq"'::regclass);
 >   ALTER TABLE public."Comments" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    202    203    203            �
           2606    21039    Cards Cards_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_pkey" PRIMARY KEY ("Id");
 >   ALTER TABLE ONLY public."Cards" DROP CONSTRAINT "Cards_pkey";
       public            postgres    false    205            �
           2606    21031    Comments Comments_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Comments"
    ADD CONSTRAINT "Comments_pkey" PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public."Comments" DROP CONSTRAINT "Comments_pkey";
       public            postgres    false    203            �
           1259    21159    card_id_index    INDEX     �   CREATE UNIQUE INDEX card_id_index ON public."Cards" USING btree ("Id") INCLUDE ("Id", "Name", "Description");

ALTER TABLE public."Cards" CLUSTER ON card_id_index;
 !   DROP INDEX public.card_id_index;
       public            postgres    false    205    205    205    205            �
           1259    21161    card_name_index    INDEX     D   CREATE INDEX card_name_index ON public."Cards" USING hash ("Name");
 #   DROP INDEX public.card_name_index;
       public            postgres    false    205            �
           1259    21160    comment_id_index    INDEX     �   CREATE UNIQUE INDEX comment_id_index ON public."Comments" USING btree ("Id") INCLUDE ("Id", "Value", "CardId");

ALTER TABLE public."Comments" CLUSTER ON comment_id_index;
 $   DROP INDEX public.comment_id_index;
       public            postgres    false    203    203    203    203            �
           2606    21040    Comments CommentsToCards_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Comments"
    ADD CONSTRAINT "CommentsToCards_fkey" FOREIGN KEY ("CardId") REFERENCES public."Cards"("Id") NOT VALID;
 K   ALTER TABLE ONLY public."Comments" DROP CONSTRAINT "CommentsToCards_fkey";
       public          postgres    false    203    205    2704           