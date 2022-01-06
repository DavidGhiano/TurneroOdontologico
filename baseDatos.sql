CREATE DATABASE  IF NOT EXISTS `turneroodontologo` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_czech_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `turneroodontologo`;
-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: localhost    Database: turneroodontologo
-- ------------------------------------------------------
-- Server version	8.0.27

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `historial`
--

DROP TABLE IF EXISTS `historial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `historial` (
  `idHistorial` int NOT NULL AUTO_INCREMENT,
  `causa` varchar(100) COLLATE utf8_czech_ci NOT NULL,
  `solucion` varchar(100) COLLATE utf8_czech_ci DEFAULT NULL,
  `idPaciente` int NOT NULL,
  PRIMARY KEY (`idHistorial`,`idPaciente`),
  KEY `fk_Historial_Paciente1_idx` (`idPaciente`),
  CONSTRAINT `fk_Historial_Paciente1` FOREIGN KEY (`idPaciente`) REFERENCES `paciente` (`idPaciente`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_czech_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `historial`
--

LOCK TABLES `historial` WRITE;
/*!40000 ALTER TABLE `historial` DISABLE KEYS */;
INSERT INTO `historial` VALUES (1,'dolor de muela','',1),(2,'caida de un diente','',1),(3,'Dolor de muela',NULL,1),(4,'Dolor de muela',NULL,2),(5,'Consulta',NULL,2),(6,'Limpieza de sarro',NULL,1),(7,'Dolores','El paciente debe tomar analgésicos por una semana cada 12 hs - Dr Milazo',2),(8,'No se',NULL,1),(9,'Muela picada','Se le extrajo la muela',3),(10,'Revisión de rutina','Se hizo una revision sin encontrar dificultad alguna',9);
/*!40000 ALTER TABLE `historial` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `paciente`
--

DROP TABLE IF EXISTS `paciente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `paciente` (
  `idPaciente` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) COLLATE utf8_czech_ci NOT NULL,
  `Apellido` varchar(45) COLLATE utf8_czech_ci NOT NULL,
  `dni` varchar(9) COLLATE utf8_czech_ci NOT NULL,
  `domicilio` varchar(100) COLLATE utf8_czech_ci NOT NULL,
  `telefono` varchar(16) COLLATE utf8_czech_ci NOT NULL,
  `fechaNacimiento` date NOT NULL,
  PRIMARY KEY (`idPaciente`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_czech_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `paciente`
--

LOCK TABLES `paciente` WRITE;
/*!40000 ALTER TABLE `paciente` DISABLE KEYS */;
INSERT INTO `paciente` VALUES (1,'David','Ghiano','44222555','Ituzaingo 750','(353)409-0644','1990-09-26'),(2,'Esteban','Quito','12345678','Av Siempreviva 456','(351)222-3333','2000-05-25'),(3,'Pedro','Picapiedra','22222222','Marmol 555','(111)222-3333','1995-02-15'),(4,'Fabiana','Alves Siqueira','40151042','Peluffo 1428','1170890436','1996-09-28'),(5,'Kevin','Salatino','36178034','Av Santa fe 4815','1170846783','1992-03-26'),(6,'Lucas','Ruiz','41673042','Alvear 124','1154678290','1996-04-14'),(7,'Trinidad','Yamul','42161971','Santa Fe 65p','3535622309','1999-09-18'),(8,'Lucio','Martínez','39851139','Caseros 15','3329327155','1996-10-13'),(9,'Pedro','Martinez','66665555','Sarmiento 754','(111)222-3333','2009-06-16');
/*!40000 ALTER TABLE `paciente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `turno`
--

DROP TABLE IF EXISTS `turno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `turno` (
  `idTurno` int NOT NULL AUTO_INCREMENT,
  `fecha` date NOT NULL,
  `hora` varchar(5) COLLATE utf8_czech_ci NOT NULL,
  `idPaciente` int NOT NULL,
  `estado` enum('ausente','concretado','en espera') COLLATE utf8_czech_ci NOT NULL,
  `idHistorial` int NOT NULL,
  PRIMARY KEY (`idTurno`,`idPaciente`,`idHistorial`),
  KEY `fk_Turno_Paciente_idx` (`idPaciente`),
  KEY `fk_Turno_Historial1_idx` (`idHistorial`),
  CONSTRAINT `fk_Turno_Historial1` FOREIGN KEY (`idHistorial`) REFERENCES `historial` (`idHistorial`),
  CONSTRAINT `fk_Turno_Paciente` FOREIGN KEY (`idPaciente`) REFERENCES `paciente` (`idPaciente`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_czech_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turno`
--

LOCK TABLES `turno` WRITE;
/*!40000 ALTER TABLE `turno` DISABLE KEYS */;
INSERT INTO `turno` VALUES (1,'2021-11-16','10:00',1,'concretado',1),(2,'2021-11-16','11:00',1,'concretado',2),(3,'2021-11-16','12:00',1,'ausente',3),(4,'2021-11-18','11:00',2,'ausente',4),(5,'2021-12-02','10:00',2,'en espera',5),(6,'2021-11-18','14:00',1,'ausente',6),(7,'2021-11-18','10:00',2,'concretado',7),(8,'2021-11-18','09:30',1,'ausente',8),(9,'2021-11-18','10:30',3,'concretado',9),(10,'2021-11-19','10:30',9,'concretado',10);
/*!40000 ALTER TABLE `turno` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-01-06  0:25:16
