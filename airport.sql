-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Jul 17, 2018 at 01:27 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `airport`
--
CREATE DATABASE IF NOT EXISTS `airport` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `airport`;

-- --------------------------------------------------------

--
-- Table structure for table `cities`
--

CREATE TABLE `cities` (
  `id` int(11) NOT NULL,
  `city` varchar(255) DEFAULT NULL,
  `state` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `cities`
--

INSERT INTO `cities` (`id`, `city`, `state`) VALUES
(1, 'Seattle', 'WA'),
(2, 'Portland', 'OR'),
(3, 'Willis', 'BR'),
(4, 'Talkin', 'Willis'),
(6, 'Bainbridge', 'WA'),
(7, 'Wallawalla', 'WA'),
(8, 'g', 'dfsf'),
(9, 'New Seattle', 'WA');

-- --------------------------------------------------------

--
-- Table structure for table `flights`
--

CREATE TABLE `flights` (
  `id` int(11) NOT NULL,
  `flight_number` int(11) DEFAULT NULL,
  `depart_time` varchar(11) DEFAULT NULL,
  `depart_id` int(11) DEFAULT NULL,
  `arrive_id` int(11) DEFAULT NULL,
  `status` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `flights`
--

INSERT INTO `flights` (`id`, `flight_number`, `depart_time`, `depart_id`, `arrive_id`, `status`) VALUES
(1, 307, '3:00 PM', 1, 2, 'on time'),
(2, 308, '7:00 PM', 3, 4, 'KIA'),
(3, 102, '2:00 PM', 2, 1, '1'),
(4, 102, '2:00 PM', 2, 1, 'xnld'),
(5, 102, '2:00 PM', 2, 1, 'xnld'),
(6, 102, '2:00 PM', 2, 1, 'xnld'),
(7, 1, '2:00 PM', 1, 1, 'sdf'),
(8, 222, '5:00pm', 22, 22222, '22323dx'),
(9, 3333, '6:00pm', 0, 0, 'open'),
(10, 333, '3:00pm', 322, 23, 'xxx'),
(11, 123, '1:00pm', 1, 2, 'on time'),
(12, 123, 'now', 9, 1, 'good to go!');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cities`
--
ALTER TABLE `cities`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `flights`
--
ALTER TABLE `flights`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cities`
--
ALTER TABLE `cities`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT for table `flights`
--
ALTER TABLE `flights`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
