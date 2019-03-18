--
-- PostgreSQL database dump
--

-- Dumped from database version 10.6
-- Dumped by pg_dump version 11.2

-- Started on 2019-03-18 14:21:47 EET

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2 (class 3079 OID 16723)
-- Name: timescaledb; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS timescaledb WITH SCHEMA public;


--
-- TOC entry 3218 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION timescaledb; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION timescaledb IS 'Enables scalable inserts and complex queries for time-series data';


SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 235 (class 1259 OID 17095)
-- Name: environment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.environment (
    id bigint NOT NULL,
    "timestamp" timestamp without time zone NOT NULL,
    temperature double precision,
    humidity double precision,
    motion boolean,
    sensor_id bigint,
    room_id bigint
);


ALTER TABLE public.environment OWNER TO postgres;

--
-- TOC entry 234 (class 1259 OID 17093)
-- Name: environment_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.environment_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.environment_id_seq OWNER TO postgres;

--
-- TOC entry 3219 (class 0 OID 0)
-- Dependencies: 234
-- Name: environment_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.environment_id_seq OWNED BY public.environment.id;


--
-- TOC entry 231 (class 1259 OID 17074)
-- Name: homes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.homes (
    id bigint NOT NULL,
    name character varying(64) NOT NULL,
    address character varying(128),
    city character varying(64),
    country character varying(64)
);


ALTER TABLE public.homes OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 17072)
-- Name: homes_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.homes_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.homes_id_seq OWNER TO postgres;

--
-- TOC entry 3220 (class 0 OID 0)
-- Dependencies: 230
-- Name: homes_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.homes_id_seq OWNED BY public.homes.id;


--
-- TOC entry 233 (class 1259 OID 17082)
-- Name: rooms; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.rooms (
    id bigint NOT NULL,
    name character varying(128),
    home_id bigint
);


ALTER TABLE public.rooms OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 17080)
-- Name: rooms_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.rooms_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.rooms_id_seq OWNER TO postgres;

--
-- TOC entry 3221 (class 0 OID 0)
-- Dependencies: 232
-- Name: rooms_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.rooms_id_seq OWNED BY public.rooms.id;


--
-- TOC entry 237 (class 1259 OID 17103)
-- Name: sensors; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sensors (
    id bigint NOT NULL,
    type character varying(64) NOT NULL,
    room_id bigint
);


ALTER TABLE public.sensors OWNER TO postgres;

--
-- TOC entry 236 (class 1259 OID 17101)
-- Name: sensors_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.sensors_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.sensors_id_seq OWNER TO postgres;

--
-- TOC entry 3222 (class 0 OID 0)
-- Dependencies: 236
-- Name: sensors_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.sensors_id_seq OWNED BY public.sensors.id;


--
-- TOC entry 239 (class 1259 OID 17127)
-- Name: weather; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.weather (
    id bigint NOT NULL,
    "timestamp" timestamp without time zone,
    pressure double precision,
    humidity double precision,
    minimum_temperature double precision,
    maximum_temperature double precision,
    condition_code bigint,
    condition character varying(512),
    icon_url character varying(128),
    home_id bigint,
    temperature double precision
);


ALTER TABLE public.weather OWNER TO postgres;

--
-- TOC entry 238 (class 1259 OID 17125)
-- Name: weather_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.weather_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.weather_id_seq OWNER TO postgres;

--
-- TOC entry 3223 (class 0 OID 0)
-- Dependencies: 238
-- Name: weather_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.weather_id_seq OWNED BY public.weather.id;


--
-- TOC entry 3021 (class 2604 OID 17098)
-- Name: environment id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.environment ALTER COLUMN id SET DEFAULT nextval('public.environment_id_seq'::regclass);


--
-- TOC entry 3019 (class 2604 OID 17077)
-- Name: homes id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.homes ALTER COLUMN id SET DEFAULT nextval('public.homes_id_seq'::regclass);


--
-- TOC entry 3020 (class 2604 OID 17085)
-- Name: rooms id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rooms ALTER COLUMN id SET DEFAULT nextval('public.rooms_id_seq'::regclass);


--
-- TOC entry 3022 (class 2604 OID 17106)
-- Name: sensors id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sensors ALTER COLUMN id SET DEFAULT nextval('public.sensors_id_seq'::regclass);


--
-- TOC entry 3023 (class 2604 OID 17130)
-- Name: weather id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.weather ALTER COLUMN id SET DEFAULT nextval('public.weather_id_seq'::regclass);


--
-- TOC entry 3076 (class 2606 OID 17100)
-- Name: environment environment_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.environment
    ADD CONSTRAINT environment_pkey PRIMARY KEY (id);


--
-- TOC entry 3072 (class 2606 OID 17079)
-- Name: homes homes_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.homes
    ADD CONSTRAINT homes_pkey PRIMARY KEY (id);


--
-- TOC entry 3074 (class 2606 OID 17087)
-- Name: rooms rooms_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rooms
    ADD CONSTRAINT rooms_pkey PRIMARY KEY (id);


--
-- TOC entry 3079 (class 2606 OID 17108)
-- Name: sensors sensors_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sensors
    ADD CONSTRAINT sensors_pkey PRIMARY KEY (id);


--
-- TOC entry 3081 (class 2606 OID 17135)
-- Name: weather weather_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.weather
    ADD CONSTRAINT weather_pkey PRIMARY KEY (id);


--
-- TOC entry 3077 (class 1259 OID 17124)
-- Name: fki_fk_rooms; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_fk_rooms ON public.environment USING btree (room_id);


--
-- TOC entry 3082 (class 2606 OID 17088)
-- Name: rooms fk_homes; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rooms
    ADD CONSTRAINT fk_homes FOREIGN KEY (home_id) REFERENCES public.homes(id);


--
-- TOC entry 3086 (class 2606 OID 17136)
-- Name: weather fk_homes; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.weather
    ADD CONSTRAINT fk_homes FOREIGN KEY (home_id) REFERENCES public.homes(id);


--
-- TOC entry 3085 (class 2606 OID 17109)
-- Name: sensors fk_room; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sensors
    ADD CONSTRAINT fk_room FOREIGN KEY (room_id) REFERENCES public.rooms(id);


--
-- TOC entry 3083 (class 2606 OID 17119)
-- Name: environment fk_rooms; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.environment
    ADD CONSTRAINT fk_rooms FOREIGN KEY (room_id) REFERENCES public.rooms(id);


--
-- TOC entry 3084 (class 2606 OID 17114)
-- Name: environment fk_sensors; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.environment
    ADD CONSTRAINT fk_sensors FOREIGN KEY (sensor_id) REFERENCES public.sensors(id);


-- Completed on 2019-03-18 14:21:48 EET

--
-- PostgreSQL database dump complete
--
