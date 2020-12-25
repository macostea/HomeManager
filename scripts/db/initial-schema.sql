PGDMP         6                x            home-manager    10.7    10.7 �    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false            �           1262    17213    home-manager    DATABASE     ~   CREATE DATABASE "home-manager" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'en_US.utf8' LC_CTYPE = 'en_US.utf8';
    DROP DATABASE "home-manager";
             postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
             postgres    false            �           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                  postgres    false    13            �           0    0    SCHEMA public    ACL     &   GRANT ALL ON SCHEMA public TO PUBLIC;
                  postgres    false    13                        3079    16797    timescaledb 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS timescaledb WITH SCHEMA public;
    DROP EXTENSION timescaledb;
                  false    13            �           0    0    EXTENSION timescaledb    COMMENT     i   COMMENT ON EXTENSION timescaledb IS 'Enables scalable inserts and complex queries for time-series data';
                       false    4                        3079    13001    plpgsql 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;
    DROP EXTENSION plpgsql;
                  false            �           0    0    EXTENSION plpgsql    COMMENT     @   COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';
                       false    1                        3079    17214    hstore 	   EXTENSION     :   CREATE EXTENSION IF NOT EXISTS hstore WITH SCHEMA public;
    DROP EXTENSION hstore;
                  false    13            �           0    0    EXTENSION hstore    COMMENT     S   COMMENT ON EXTENSION hstore IS 'data type for storing sets of (key, value) pairs';
                       false    3                        3079    17337 	   uuid-ossp 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;
    DROP EXTENSION "uuid-ossp";
                  false    13            �           0    0    EXTENSION "uuid-ossp"    COMMENT     W   COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';
                       false    2            �            1259    17348    environment    TABLE     &  CREATE TABLE public.environment (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "timestamp" timestamp without time zone NOT NULL,
    temperature double precision,
    humidity double precision,
    motion boolean,
    sensor_id uuid,
    room_id uuid,
    light public.hstore
);
    DROP TABLE public.environment;
       public         postgres    false    2    13    13    3    3    13    3    13    3    13    3    13    13            �            1259    17355    _hyper_2_10_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_10_chunk (
    CONSTRAINT constraint_10 CHECK ((("timestamp" >= '2019-06-20 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-06-27 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_10_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17363    _hyper_2_11_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_11_chunk (
    CONSTRAINT constraint_11 CHECK ((("timestamp" >= '2019-06-27 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-07-04 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_11_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17371    _hyper_2_12_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_12_chunk (
    CONSTRAINT constraint_12 CHECK ((("timestamp" >= '2019-07-04 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-07-11 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_12_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17379    _hyper_2_13_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_13_chunk (
    CONSTRAINT constraint_13 CHECK ((("timestamp" >= '2019-07-11 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-07-18 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_13_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17387    _hyper_2_14_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_14_chunk (
    CONSTRAINT constraint_14 CHECK ((("timestamp" >= '2019-07-25 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-08-01 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_14_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17395    _hyper_2_15_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_15_chunk (
    CONSTRAINT constraint_15 CHECK ((("timestamp" >= '2019-08-01 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-08-08 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_15_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17403    _hyper_2_16_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_16_chunk (
    CONSTRAINT constraint_16 CHECK ((("timestamp" >= '2019-08-08 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-08-15 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_16_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17411    _hyper_2_17_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_17_chunk (
    CONSTRAINT constraint_17 CHECK ((("timestamp" >= '2019-08-15 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-08-22 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_17_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17419    _hyper_2_18_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_18_chunk (
    CONSTRAINT constraint_18 CHECK ((("timestamp" >= '2019-08-22 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-08-29 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_18_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17427    _hyper_2_19_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_19_chunk (
    CONSTRAINT constraint_19 CHECK ((("timestamp" >= '2019-08-29 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-09-05 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_19_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17435    _hyper_2_1_chunk    TABLE       CREATE TABLE _timescaledb_internal._hyper_2_1_chunk (
    CONSTRAINT constraint_1 CHECK ((("timestamp" >= '2019-04-04 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-04-11 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 3   DROP TABLE _timescaledb_internal._hyper_2_1_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17443    _hyper_2_20_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_20_chunk (
    CONSTRAINT constraint_20 CHECK ((("timestamp" >= '2019-09-05 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-09-12 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_20_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17451    _hyper_2_21_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_21_chunk (
    CONSTRAINT constraint_21 CHECK ((("timestamp" >= '2019-09-12 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-09-19 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_21_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17459    _hyper_2_22_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_22_chunk (
    CONSTRAINT constraint_22 CHECK ((("timestamp" >= '2019-09-19 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-09-26 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_22_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17467    _hyper_2_23_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_23_chunk (
    CONSTRAINT constraint_23 CHECK ((("timestamp" >= '2019-09-26 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-10-03 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_23_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17475    _hyper_2_24_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_24_chunk (
    CONSTRAINT constraint_24 CHECK ((("timestamp" >= '2019-10-03 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-10-10 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_24_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            �            1259    17483    _hyper_2_25_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_25_chunk (
    CONSTRAINT constraint_25 CHECK ((("timestamp" >= '2019-10-10 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-10-17 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_25_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                        1259    17491    _hyper_2_26_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_26_chunk (
    CONSTRAINT constraint_26 CHECK ((("timestamp" >= '2019-10-17 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-10-24 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_26_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17499    _hyper_2_27_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_27_chunk (
    CONSTRAINT constraint_27 CHECK ((("timestamp" >= '2019-10-24 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-10-31 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_27_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17507    _hyper_2_28_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_28_chunk (
    CONSTRAINT constraint_28 CHECK ((("timestamp" >= '2019-10-31 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-11-07 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_28_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17515    _hyper_2_29_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_29_chunk (
    CONSTRAINT constraint_29 CHECK ((("timestamp" >= '2019-11-07 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-11-14 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_29_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17523    _hyper_2_2_chunk    TABLE       CREATE TABLE _timescaledb_internal._hyper_2_2_chunk (
    CONSTRAINT constraint_2 CHECK ((("timestamp" >= '2019-04-11 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-04-18 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 3   DROP TABLE _timescaledb_internal._hyper_2_2_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17531    _hyper_2_30_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_30_chunk (
    CONSTRAINT constraint_30 CHECK ((("timestamp" >= '2019-11-14 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-11-21 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_30_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17539    _hyper_2_31_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_31_chunk (
    CONSTRAINT constraint_31 CHECK ((("timestamp" >= '2019-11-21 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-11-28 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_31_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17547    _hyper_2_32_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_32_chunk (
    CONSTRAINT constraint_32 CHECK ((("timestamp" >= '2019-11-28 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-12-05 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_32_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17555    _hyper_2_33_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_33_chunk (
    CONSTRAINT constraint_33 CHECK ((("timestamp" >= '2019-12-05 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-12-12 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_33_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            	           1259    17563    _hyper_2_34_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_34_chunk (
    CONSTRAINT constraint_34 CHECK ((("timestamp" >= '2020-02-27 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-03-05 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_34_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            
           1259    17571    _hyper_2_35_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_35_chunk (
    CONSTRAINT constraint_35 CHECK ((("timestamp" >= '2020-03-26 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-04-02 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_35_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17579    _hyper_2_36_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_36_chunk (
    CONSTRAINT constraint_36 CHECK ((("timestamp" >= '2020-04-02 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-04-09 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_36_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17587    _hyper_2_37_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_37_chunk (
    CONSTRAINT constraint_37 CHECK ((("timestamp" >= '2020-04-09 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-04-16 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_37_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17595    _hyper_2_38_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_38_chunk (
    CONSTRAINT constraint_38 CHECK ((("timestamp" >= '2020-04-23 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-04-30 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_38_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17603    _hyper_2_39_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_39_chunk (
    CONSTRAINT constraint_39 CHECK ((("timestamp" >= '2020-04-30 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-05-07 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_39_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17611    _hyper_2_3_chunk    TABLE       CREATE TABLE _timescaledb_internal._hyper_2_3_chunk (
    CONSTRAINT constraint_3 CHECK ((("timestamp" >= '2019-04-18 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-04-25 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 3   DROP TABLE _timescaledb_internal._hyper_2_3_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17619    _hyper_2_40_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_40_chunk (
    CONSTRAINT constraint_40 CHECK ((("timestamp" >= '2020-05-07 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-05-14 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_40_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17627    _hyper_2_41_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_41_chunk (
    CONSTRAINT constraint_41 CHECK ((("timestamp" >= '2020-05-14 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-05-21 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_41_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17635    _hyper_2_42_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_42_chunk (
    CONSTRAINT constraint_42 CHECK ((("timestamp" >= '2020-05-21 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-05-28 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_42_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17643    _hyper_2_43_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_43_chunk (
    CONSTRAINT constraint_43 CHECK ((("timestamp" >= '2020-07-02 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-07-09 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_43_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17651    _hyper_2_44_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_44_chunk (
    CONSTRAINT constraint_44 CHECK ((("timestamp" >= '2020-07-09 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-07-16 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_44_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17659    _hyper_2_45_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_45_chunk (
    CONSTRAINT constraint_45 CHECK ((("timestamp" >= '2020-07-16 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-07-23 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_45_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17667    _hyper_2_46_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_46_chunk (
    CONSTRAINT constraint_46 CHECK ((("timestamp" >= '2020-07-23 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-07-30 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_46_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17675    _hyper_2_47_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_47_chunk (
    CONSTRAINT constraint_47 CHECK ((("timestamp" >= '2020-07-30 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-08-06 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_47_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17683    _hyper_2_48_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_48_chunk (
    CONSTRAINT constraint_48 CHECK ((("timestamp" >= '2020-08-06 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-08-13 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_48_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17691    _hyper_2_49_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_49_chunk (
    CONSTRAINT constraint_49 CHECK ((("timestamp" >= '2020-08-13 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-08-20 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_49_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17699    _hyper_2_4_chunk    TABLE       CREATE TABLE _timescaledb_internal._hyper_2_4_chunk (
    CONSTRAINT constraint_4 CHECK ((("timestamp" >= '2019-04-25 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-05-02 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 3   DROP TABLE _timescaledb_internal._hyper_2_4_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17707    _hyper_2_50_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_50_chunk (
    CONSTRAINT constraint_50 CHECK ((("timestamp" >= '2020-08-20 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-08-27 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_50_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17715    _hyper_2_51_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_51_chunk (
    CONSTRAINT constraint_51 CHECK ((("timestamp" >= '2020-08-27 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-09-03 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_51_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17723    _hyper_2_52_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_52_chunk (
    CONSTRAINT constraint_52 CHECK ((("timestamp" >= '2020-09-17 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-09-24 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_52_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17731    _hyper_2_53_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_53_chunk (
    CONSTRAINT constraint_53 CHECK ((("timestamp" >= '2020-09-24 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-10-01 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_53_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                       1259    17739    _hyper_2_54_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_54_chunk (
    CONSTRAINT constraint_54 CHECK ((("timestamp" >= '2020-10-01 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-10-08 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_54_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13                        1259    17747    _hyper_2_55_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_55_chunk (
    CONSTRAINT constraint_55 CHECK ((("timestamp" >= '2020-10-08 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-10-15 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_55_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            !           1259    17755    _hyper_2_56_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_56_chunk (
    CONSTRAINT constraint_56 CHECK ((("timestamp" >= '2020-10-15 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-10-22 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_56_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            "           1259    17763    _hyper_2_57_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_57_chunk (
    CONSTRAINT constraint_57 CHECK ((("timestamp" >= '2020-10-22 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-10-29 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_57_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            #           1259    17771    _hyper_2_58_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_58_chunk (
    CONSTRAINT constraint_58 CHECK ((("timestamp" >= '2020-10-29 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-11-05 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_58_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            $           1259    17779    _hyper_2_59_chunk    TABLE     
  CREATE TABLE _timescaledb_internal._hyper_2_59_chunk (
    CONSTRAINT constraint_59 CHECK ((("timestamp" >= '2020-11-05 00:00:00'::timestamp without time zone) AND ("timestamp" < '2020-11-12 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 4   DROP TABLE _timescaledb_internal._hyper_2_59_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            %           1259    17787    _hyper_2_5_chunk    TABLE       CREATE TABLE _timescaledb_internal._hyper_2_5_chunk (
    CONSTRAINT constraint_5 CHECK ((("timestamp" >= '2019-05-02 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-05-09 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 3   DROP TABLE _timescaledb_internal._hyper_2_5_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            &           1259    17795    _hyper_2_6_chunk    TABLE       CREATE TABLE _timescaledb_internal._hyper_2_6_chunk (
    CONSTRAINT constraint_6 CHECK ((("timestamp" >= '2019-05-09 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-05-16 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 3   DROP TABLE _timescaledb_internal._hyper_2_6_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            '           1259    17803    _hyper_2_7_chunk    TABLE       CREATE TABLE _timescaledb_internal._hyper_2_7_chunk (
    CONSTRAINT constraint_7 CHECK ((("timestamp" >= '2019-05-23 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-05-30 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 3   DROP TABLE _timescaledb_internal._hyper_2_7_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            (           1259    17811    _hyper_2_8_chunk    TABLE       CREATE TABLE _timescaledb_internal._hyper_2_8_chunk (
    CONSTRAINT constraint_8 CHECK ((("timestamp" >= '2019-05-30 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-06-06 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 3   DROP TABLE _timescaledb_internal._hyper_2_8_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            )           1259    17819    _hyper_2_9_chunk    TABLE       CREATE TABLE _timescaledb_internal._hyper_2_9_chunk (
    CONSTRAINT constraint_9 CHECK ((("timestamp" >= '2019-06-06 00:00:00'::timestamp without time zone) AND ("timestamp" < '2019-06-13 00:00:00'::timestamp without time zone)))
)
INHERITS (public.environment);
 3   DROP TABLE _timescaledb_internal._hyper_2_9_chunk;
       _timescaledb_internal         postgres    false    238    4    3    3    13    3    13    3    13    3    13    13            *           1259    17827    environment_id_seq    SEQUENCE     {   CREATE SEQUENCE public.environment_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.environment_id_seq;
       public       postgres    false    238    13            �           0    0    environment_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.environment_id_seq OWNED BY public.environment.id;
            public       postgres    false    298            +           1259    17829    homes    TABLE     �   CREATE TABLE public.homes (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name character varying(64) NOT NULL,
    address character varying(128),
    city character varying(64),
    country character varying(64)
);
    DROP TABLE public.homes;
       public         postgres    false    2    13    13            ,           1259    17833    homes_id_seq    SEQUENCE     u   CREATE SEQUENCE public.homes_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.homes_id_seq;
       public       postgres    false    299    13            �           0    0    homes_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.homes_id_seq OWNED BY public.homes.id;
            public       postgres    false    300            -           1259    17835    homeyMappings    TABLE     �   CREATE TABLE public."homeyMappings" (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    sensor_id uuid NOT NULL,
    temp_topic character varying(256),
    hum_topic character varying(256),
    motion_topic character varying(256)
);
 #   DROP TABLE public."homeyMappings";
       public         postgres    false    2    13    13            .           1259    17842    rooms    TABLE     �   CREATE TABLE public.rooms (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name character varying(128),
    home_id uuid
);
    DROP TABLE public.rooms;
       public         postgres    false    2    13    13            /           1259    17846    rooms_id_seq    SEQUENCE     u   CREATE SEQUENCE public.rooms_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.rooms_id_seq;
       public       postgres    false    302    13            �           0    0    rooms_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.rooms_id_seq OWNED BY public.rooms.id;
            public       postgres    false    303            0           1259    17848    sensors    TABLE     �   CREATE TABLE public.sensors (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    type character varying(64) NOT NULL,
    room_id uuid
);
    DROP TABLE public.sensors;
       public         postgres    false    2    13    13            1           1259    17852    sensors_id_seq    SEQUENCE     w   CREATE SEQUENCE public.sensors_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.sensors_id_seq;
       public       postgres    false    304    13            �           0    0    sensors_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.sensors_id_seq OWNED BY public.sensors.id;
            public       postgres    false    305            2           1259    17854    weather    TABLE     �  CREATE TABLE public.weather (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    "timestamp" timestamp without time zone,
    pressure double precision,
    humidity double precision,
    minimum_temperature double precision,
    maximum_temperature double precision,
    condition_code bigint,
    condition character varying(512),
    icon_url character varying(128),
    home_id uuid,
    temperature double precision
);
    DROP TABLE public.weather;
       public         postgres    false    2    13    13            3           1259    17861    weather_id_seq    SEQUENCE     w   CREATE SEQUENCE public.weather_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.weather_id_seq;
       public       postgres    false    306    13            �           0    0    weather_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.weather_id_seq OWNED BY public.weather.id;
            public       postgres    false    307            �           2604    17863    _hyper_2_10_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_10_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_10_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    239            �           2604    17864    _hyper_2_11_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_11_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_11_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    240            �           2604    17865    _hyper_2_12_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_12_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_12_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    241            �           2604    17866    _hyper_2_13_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_13_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_13_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    242            �           2604    17867    _hyper_2_14_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_14_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_14_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    243            �           2604    17868    _hyper_2_15_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_15_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_15_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    244            �           2604    17869    _hyper_2_16_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_16_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_16_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    245            �           2604    17870    _hyper_2_17_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_17_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_17_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    246            �           2604    17871    _hyper_2_18_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_18_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_18_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    247            �           2604    17872    _hyper_2_19_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_19_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_19_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    248            �           2604    17873    _hyper_2_1_chunk id    DEFAULT     o   ALTER TABLE ONLY _timescaledb_internal._hyper_2_1_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 Q   ALTER TABLE _timescaledb_internal._hyper_2_1_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    249            �           2604    17874    _hyper_2_20_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_20_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_20_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    250            �           2604    17875    _hyper_2_21_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_21_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_21_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    251            �           2604    17876    _hyper_2_22_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_22_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_22_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    252            �           2604    17877    _hyper_2_23_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_23_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_23_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    253            �           2604    17878    _hyper_2_24_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_24_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_24_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    254            �           2604    17879    _hyper_2_25_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_25_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_25_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    255            �           2604    17880    _hyper_2_26_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_26_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_26_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    256            �           2604    17881    _hyper_2_27_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_27_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_27_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    257            �           2604    17882    _hyper_2_28_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_28_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_28_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    258            �           2604    17883    _hyper_2_29_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_29_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_29_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    259            �           2604    17884    _hyper_2_2_chunk id    DEFAULT     o   ALTER TABLE ONLY _timescaledb_internal._hyper_2_2_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 Q   ALTER TABLE _timescaledb_internal._hyper_2_2_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    260            �           2604    17885    _hyper_2_30_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_30_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_30_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    261            �           2604    17886    _hyper_2_31_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_31_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_31_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    262            �           2604    17887    _hyper_2_32_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_32_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_32_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    263            �           2604    17888    _hyper_2_33_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_33_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_33_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    264            �           2604    17889    _hyper_2_34_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_34_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_34_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    265            �           2604    17890    _hyper_2_35_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_35_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_35_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    266            �           2604    17891    _hyper_2_36_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_36_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_36_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    267            �           2604    17892    _hyper_2_37_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_37_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_37_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    268            �           2604    17893    _hyper_2_38_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_38_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_38_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    269            �           2604    17894    _hyper_2_39_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_39_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_39_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    270            �           2604    17895    _hyper_2_3_chunk id    DEFAULT     o   ALTER TABLE ONLY _timescaledb_internal._hyper_2_3_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 Q   ALTER TABLE _timescaledb_internal._hyper_2_3_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    271            �           2604    17896    _hyper_2_40_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_40_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_40_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    272            �           2604    17897    _hyper_2_41_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_41_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_41_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    273            �           2604    17898    _hyper_2_42_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_42_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_42_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    274            �           2604    17899    _hyper_2_43_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_43_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_43_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    275            �           2604    17900    _hyper_2_44_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_44_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_44_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    276            �           2604    17901    _hyper_2_45_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_45_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_45_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    277            �           2604    17902    _hyper_2_46_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_46_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_46_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    278            �           2604    17903    _hyper_2_47_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_47_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_47_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    279            �           2604    17904    _hyper_2_48_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_48_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_48_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    280            �           2604    17905    _hyper_2_49_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_49_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_49_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    281            �           2604    17906    _hyper_2_4_chunk id    DEFAULT     o   ALTER TABLE ONLY _timescaledb_internal._hyper_2_4_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 Q   ALTER TABLE _timescaledb_internal._hyper_2_4_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    282            �           2604    17907    _hyper_2_50_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_50_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_50_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    283            �           2604    17908    _hyper_2_51_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_51_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_51_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    284            �           2604    17909    _hyper_2_52_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_52_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_52_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    285            �           2604    17910    _hyper_2_53_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_53_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_53_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    286            �           2604    17911    _hyper_2_54_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_54_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_54_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    287            �           2604    17912    _hyper_2_55_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_55_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_55_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    288            �           2604    17913    _hyper_2_56_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_56_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_56_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    289            �           2604    17914    _hyper_2_57_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_57_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_57_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    290            �           2604    17915    _hyper_2_58_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_58_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_58_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    291            �           2604    17916    _hyper_2_59_chunk id    DEFAULT     p   ALTER TABLE ONLY _timescaledb_internal._hyper_2_59_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 R   ALTER TABLE _timescaledb_internal._hyper_2_59_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    292            �           2604    17917    _hyper_2_5_chunk id    DEFAULT     o   ALTER TABLE ONLY _timescaledb_internal._hyper_2_5_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 Q   ALTER TABLE _timescaledb_internal._hyper_2_5_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    293            �           2604    17918    _hyper_2_6_chunk id    DEFAULT     o   ALTER TABLE ONLY _timescaledb_internal._hyper_2_6_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 Q   ALTER TABLE _timescaledb_internal._hyper_2_6_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    294            �           2604    17919    _hyper_2_7_chunk id    DEFAULT     o   ALTER TABLE ONLY _timescaledb_internal._hyper_2_7_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 Q   ALTER TABLE _timescaledb_internal._hyper_2_7_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    295                       2604    17920    _hyper_2_8_chunk id    DEFAULT     o   ALTER TABLE ONLY _timescaledb_internal._hyper_2_8_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 Q   ALTER TABLE _timescaledb_internal._hyper_2_8_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    296                       2604    17921    _hyper_2_9_chunk id    DEFAULT     o   ALTER TABLE ONLY _timescaledb_internal._hyper_2_9_chunk ALTER COLUMN id SET DEFAULT public.uuid_generate_v4();
 Q   ALTER TABLE _timescaledb_internal._hyper_2_9_chunk ALTER COLUMN id DROP DEFAULT;
       _timescaledb_internal       postgres    false    2    13    297            .           2606    17955    homes homes_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.homes
    ADD CONSTRAINT homes_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.homes DROP CONSTRAINT homes_pkey;
       public         postgres    false    299            0           2606    17957     homeyMappings homeyMappings_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public."homeyMappings"
    ADD CONSTRAINT "homeyMappings_pkey" PRIMARY KEY (id);
 N   ALTER TABLE ONLY public."homeyMappings" DROP CONSTRAINT "homeyMappings_pkey";
       public         postgres    false    301            2           2606    17959    rooms rooms_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.rooms
    ADD CONSTRAINT rooms_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.rooms DROP CONSTRAINT rooms_pkey;
       public         postgres    false    302            4           2606    17961    sensors sensors_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.sensors
    ADD CONSTRAINT sensors_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.sensors DROP CONSTRAINT sensors_pkey;
       public         postgres    false    304            6           2606    17963    weather weather_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.weather
    ADD CONSTRAINT weather_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.weather DROP CONSTRAINT weather_pkey;
       public         postgres    false    306            
           1259    17966 +   _hyper_2_10_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_10_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_10_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_10_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    239                       1259    17967    _hyper_2_10_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_10_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_10_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_10_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    239                       1259    17970 +   _hyper_2_11_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_11_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_11_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_11_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    240                       1259    17971    _hyper_2_11_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_11_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_11_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_11_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    240                       1259    17972 +   _hyper_2_12_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_12_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_12_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_12_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    241                       1259    17975    _hyper_2_12_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_12_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_12_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_12_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    241                       1259    17976 +   _hyper_2_13_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_13_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_13_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_13_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    242                       1259    17977    _hyper_2_13_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_13_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_13_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_13_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    242                       1259    17980 +   _hyper_2_14_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_14_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_14_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_14_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    243                       1259    17981    _hyper_2_14_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_14_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_14_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_14_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    243                       1259    17982 +   _hyper_2_15_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_15_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_15_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_15_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    244                       1259    17985    _hyper_2_15_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_15_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_15_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_15_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    244                       1259    17986 +   _hyper_2_16_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_16_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_16_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_16_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    245                       1259    17987    _hyper_2_16_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_16_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_16_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_16_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    245                       1259    17990 +   _hyper_2_17_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_17_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_17_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_17_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    246                       1259    17991    _hyper_2_17_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_17_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_17_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_17_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    246                       1259    17994 +   _hyper_2_18_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_18_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_18_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_18_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    247                       1259    17995    _hyper_2_18_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_18_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_18_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_18_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    247                       1259    17998 +   _hyper_2_19_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_19_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_19_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_19_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    248                       1259    17999    _hyper_2_19_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_19_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_19_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_19_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    248                       1259    18000    _hyper_2_1_chunk_fki_fk_rooms    INDEX     l   CREATE INDEX _hyper_2_1_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_1_chunk USING btree (room_id);
 @   DROP INDEX _timescaledb_internal._hyper_2_1_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    249                       1259    18001 +   _hyper_2_20_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_20_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_20_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_20_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    250                        1259    18002    _hyper_2_20_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_20_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_20_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_20_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    250            !           1259    18003 +   _hyper_2_21_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_21_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_21_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_21_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    251            "           1259    18004    _hyper_2_21_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_21_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_21_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_21_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    251            #           1259    18005 +   _hyper_2_22_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_22_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_22_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_22_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    252            $           1259    18006    _hyper_2_22_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_22_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_22_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_22_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    252            %           1259    18007 +   _hyper_2_23_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_23_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_23_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_23_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    253            &           1259    18008    _hyper_2_23_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_23_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_23_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_23_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    253            '           1259    18009 +   _hyper_2_24_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_24_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_24_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_24_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    254            (           1259    18012    _hyper_2_24_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_24_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_24_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_24_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    254            )           1259    18013 +   _hyper_2_25_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_25_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_25_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_25_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    255            *           1259    18014    _hyper_2_25_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_25_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_25_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_25_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    255            +           1259    18015 +   _hyper_2_26_chunk_environment_timestamp_idx    INDEX     �   CREATE INDEX _hyper_2_26_chunk_environment_timestamp_idx ON _timescaledb_internal._hyper_2_26_chunk USING btree ("timestamp" DESC);
 N   DROP INDEX _timescaledb_internal._hyper_2_26_chunk_environment_timestamp_idx;
       _timescaledb_internal         postgres    false    256            ,           1259    18016    _hyper_2_26_chunk_fki_fk_rooms    INDEX     n   CREATE INDEX _hyper_2_26_chunk_fki_fk_rooms ON _timescaledb_internal._hyper_2_26_chunk USING btree (room_id);
 A   DROP INDEX _timescaledb_internal._hyper_2_26_chunk_fki_fk_rooms;
       _timescaledb_internal         postgres    false    256           