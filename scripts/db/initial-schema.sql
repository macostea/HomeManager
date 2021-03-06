PGDMP     6    (            
    w            home-manager    10.7    12.0 $    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    17211    home-manager    DATABASE     ~   CREATE DATABASE "home-manager" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'en_US.utf8' LC_CTYPE = 'en_US.utf8';
    DROP DATABASE "home-manager";
                postgres    false                        3079    16797    timescaledb 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS timescaledb WITH SCHEMA public;
    DROP EXTENSION timescaledb;
                   false            �           0    0    EXTENSION timescaledb    COMMENT     i   COMMENT ON EXTENSION timescaledb IS 'Enables scalable inserts and complex queries for time-series data';
                        false    3                        3079    25403 	   uuid-ossp 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;
    DROP EXTENSION "uuid-ossp";
                   false            �           0    0    EXTENSION "uuid-ossp"    COMMENT     W   COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';
                        false    2            �            1259    17212    environment    TABLE       CREATE TABLE public.environment (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "timestamp" timestamp without time zone NOT NULL,
    temperature double precision,
    humidity double precision,
    motion boolean,
    room_id uuid,
    sensor_id uuid
);
    DROP TABLE public.environment;
       public            postgres    false    2            �            1259    17215    environment_id_seq    SEQUENCE     {   CREATE SEQUENCE public.environment_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.environment_id_seq;
       public          postgres    false    237            �           0    0    environment_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.environment_id_seq OWNED BY public.environment.id;
          public          postgres    false    238            �            1259    17217    homes    TABLE     �   CREATE TABLE public.homes (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name character varying(64) NOT NULL,
    address character varying(128),
    city character varying(64),
    country character varying(64)
);
    DROP TABLE public.homes;
       public            postgres    false    2            �            1259    17220    homes_id_seq    SEQUENCE     u   CREATE SEQUENCE public.homes_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.homes_id_seq;
       public          postgres    false    239            �           0    0    homes_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.homes_id_seq OWNED BY public.homes.id;
          public          postgres    false    240            �            1259    17222    rooms    TABLE     �   CREATE TABLE public.rooms (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name character varying(128),
    home_id uuid
);
    DROP TABLE public.rooms;
       public            postgres    false    2            �            1259    17225    rooms_id_seq    SEQUENCE     u   CREATE SEQUENCE public.rooms_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.rooms_id_seq;
       public          postgres    false    241            �           0    0    rooms_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.rooms_id_seq OWNED BY public.rooms.id;
          public          postgres    false    242            �            1259    17227    sensors    TABLE     �   CREATE TABLE public.sensors (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    type character varying(64) NOT NULL,
    room_id uuid
);
    DROP TABLE public.sensors;
       public            postgres    false    2            �            1259    17230    sensors_id_seq    SEQUENCE     w   CREATE SEQUENCE public.sensors_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.sensors_id_seq;
       public          postgres    false    243            �           0    0    sensors_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.sensors_id_seq OWNED BY public.sensors.id;
          public          postgres    false    244            �            1259    17232    weather    TABLE     �  CREATE TABLE public.weather (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "timestamp" timestamp without time zone,
    pressure double precision,
    humidity double precision,
    minimum_temperature double precision,
    maximum_temperature double precision,
    condition_code bigint,
    condition character varying(512),
    icon_url character varying(128),
    temperature double precision,
    home_id uuid
);
    DROP TABLE public.weather;
       public            postgres    false    2            �            1259    17238    weather_id_seq    SEQUENCE     w   CREATE SEQUENCE public.weather_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.weather_id_seq;
       public          postgres    false    245            �           0    0    weather_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.weather_id_seq OWNED BY public.weather.id;
          public          postgres    false    246            B           2606    25493    environment environment_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.environment
    ADD CONSTRAINT environment_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.environment DROP CONSTRAINT environment_pkey;
       public            postgres    false    237            E           2606    25421    homes homes_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.homes
    ADD CONSTRAINT homes_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.homes DROP CONSTRAINT homes_pkey;
       public            postgres    false    239            H           2606    25459    rooms rooms_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.rooms
    ADD CONSTRAINT rooms_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.rooms DROP CONSTRAINT rooms_pkey;
       public            postgres    false    241            K           2606    25479    sensors sensors_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.sensors
    ADD CONSTRAINT sensors_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.sensors DROP CONSTRAINT sensors_pkey;
       public            postgres    false    243            M           2606    25438    weather weather_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.weather
    ADD CONSTRAINT weather_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.weather DROP CONSTRAINT weather_pkey;
       public            postgres    false    245            F           1259    25453    fki_fk_homes    INDEX     A   CREATE INDEX fki_fk_homes ON public.rooms USING btree (home_id);
     DROP INDEX public.fki_fk_homes;
       public            postgres    false    241            I           1259    25472    fki_fk_room    INDEX     B   CREATE INDEX fki_fk_room ON public.sensors USING btree (room_id);
    DROP INDEX public.fki_fk_room;
       public            postgres    false    243            C           1259    25491    fki_fk_sensor    INDEX     J   CREATE INDEX fki_fk_sensor ON public.environment USING btree (sensor_id);
 !   DROP INDEX public.fki_fk_sensor;
       public            postgres    false    237            R           2606    25432    weather fk_homes    FK CONSTRAINT     o   ALTER TABLE ONLY public.weather
    ADD CONSTRAINT fk_homes FOREIGN KEY (home_id) REFERENCES public.homes(id);
 :   ALTER TABLE ONLY public.weather DROP CONSTRAINT fk_homes;
       public          postgres    false    3141    239    245            P           2606    25448    rooms fk_homes    FK CONSTRAINT     m   ALTER TABLE ONLY public.rooms
    ADD CONSTRAINT fk_homes FOREIGN KEY (home_id) REFERENCES public.homes(id);
 8   ALTER TABLE ONLY public.rooms DROP CONSTRAINT fk_homes;
       public          postgres    false    3141    241    239            Q           2606    25467    sensors fk_room    FK CONSTRAINT     n   ALTER TABLE ONLY public.sensors
    ADD CONSTRAINT fk_room FOREIGN KEY (room_id) REFERENCES public.rooms(id);
 9   ALTER TABLE ONLY public.sensors DROP CONSTRAINT fk_room;
       public          postgres    false    243    3144    241            O           2606    25473    environment fk_room    FK CONSTRAINT     r   ALTER TABLE ONLY public.environment
    ADD CONSTRAINT fk_room FOREIGN KEY (room_id) REFERENCES public.rooms(id);
 =   ALTER TABLE ONLY public.environment DROP CONSTRAINT fk_room;
       public          postgres    false    3144    237    241            N           2606    25486    environment fk_sensor    FK CONSTRAINT     x   ALTER TABLE ONLY public.environment
    ADD CONSTRAINT fk_sensor FOREIGN KEY (sensor_id) REFERENCES public.sensors(id);
 ?   ALTER TABLE ONLY public.environment DROP CONSTRAINT fk_sensor;
       public          postgres    false    3147    243    237           