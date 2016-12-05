-- --------------------------------------------------------
-- Host:                         51.254.136.157
-- Server version:               5.5.44-MariaDB - MariaDB Server
-- Server OS:                    Linux
-- HeidiSQL Version:             9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping database structure for ufndrm
CREATE DATABASE IF NOT EXISTS `ufndrm` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `ufndrm`;


-- Dumping structure for table ufndrm.codes
CREATE TABLE IF NOT EXISTS `codes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` text NOT NULL,
  `code` text NOT NULL,
  `details` text NOT NULL,
  `activated` text NOT NULL,
  `dateofactivation` text NOT NULL,
  `activator` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- Dumping data for table ufndrm.codes: 10 rows
/*!40000 ALTER TABLE `codes` DISABLE KEYS */;
REPLACE INTO `codes` (`id`, `name`, `code`, `details`, `activated`, `dateofactivation`, `activator`) VALUES
	(1, 'A new random key.', 'aWWcV-INA1T-Iqiriq-5PDPf-2kk7ws9-xfD1-RHUFN', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '1', '2015/12/15 08:36:47 AM', 'test123'),
	(2, 'A new random key.', 'VaUPX-h37NB-7w3Qtw-OeV5G-sQGJM3g-ATVk-i0UFN', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '0', '', ''),
	(3, 'A new random key.', 'hOvRt-dgFku-c9d6nv-Y1ptW-IohXaUC-mP3J-1GUFN', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '0', '', ''),
	(4, 'A new random key.', 'pjfb6-BUjHj-lxjFoO-wmpDp-EdWBebd-aj7q-IDUFN', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '0', '', ''),
	(5, 'A new random key.', 'p2jpS-1AmQR-CHcvZ8-fqOTR-AFImtFM-C2nQ-WLUFN', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '0', '', ''),
	(6, 'A new random key.', 'U6cCQ-hJ3iB-n3QMt5-A1S67-4WBx8tx-qVRm-SFUFN', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '0', '', ''),
	(7, 'A new random key.', 'U4u4Z-7mnVt-5h4CK1-hvWiS-BMlITPS-8Qd6-ZpUFN', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '0', '', ''),
	(8, 'A new random key.', 'f13Rx-wlLhU-iIU658-dd8NE-RLz5IYR-T5w9-6AUFN', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '0', '', ''),
	(9, 'A new random key.', '1D7mp-pgH7a-Ndj1qs-O5jzE-piDgcIN-lOnm-svUFN', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '0', '', ''),
	(10, 'A new random key.', 'JSUZA-2aofu-pGWeMf-OqE63-ViMJDB7-04CJ-WwUFN', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '0', '', '');
/*!40000 ALTER TABLE `codes` ENABLE KEYS */;


-- Dumping structure for table ufndrm.news
CREATE TABLE IF NOT EXISTS `news` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- Dumping data for table ufndrm.news: ~3 rows (approximately)
/*!40000 ALTER TABLE `news` DISABLE KEYS */;
REPLACE INTO `news` (`id`, `description`) VALUES
	(1, 'It works!'),
	(2, 'Added a game to demonstrate how the system should work.'),
	(3, 'Implemented an extra security layer (code obfuscation) to prevent reverse engineering.');
/*!40000 ALTER TABLE `news` ENABLE KEYS */;


-- Dumping structure for table ufndrm.servers
CREATE TABLE IF NOT EXISTS `servers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ip` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dumping data for table ufndrm.servers: ~2 rows (approximately)
/*!40000 ALTER TABLE `servers` DISABLE KEYS */;
REPLACE INTO `servers` (`id`, `ip`) VALUES
	(1, '51.254.136.157');
/*!40000 ALTER TABLE `servers` ENABLE KEYS */;


-- Dumping structure for table ufndrm.updates
CREATE TABLE IF NOT EXISTS `updates` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `link` text NOT NULL,
  `version` text NOT NULL,
  `details` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table ufndrm.updates: ~0 rows (approximately)
/*!40000 ALTER TABLE `updates` DISABLE KEYS */;
/*!40000 ALTER TABLE `updates` ENABLE KEYS */;


-- Dumping structure for table ufndrm.users
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` text NOT NULL,
  `password` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `rank` text NOT NULL,
  `loggedin` int(11) NOT NULL DEFAULT '0',
  `expirydate` text,
  `sq` text NOT NULL,
  `sa` text NOT NULL,
  `email` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dumping data for table ufndrm.users: 2 rows
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
REPLACE INTO `users` (`id`, `username`, `password`, `rank`, `loggedin`, `expirydate`, `sq`, `sa`, `email`) VALUES
	(1, 'admin', 'KrbZoE/ifsQ=', 'Administrator', 0, '2015/12/17 11:44:41 AM', 'What is your favorite game?', 'LWSedNeH3ds=', 'admin@ufndrm.net'),
	(2, 'test123', 'KrbZoE/ifsQ=', 'VIP Member', 0, '2016/01/15 08:36:47 AM', 'What is your favorite game?', 'LWSedNeH3ds=', 'test@ufndrm.net');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
