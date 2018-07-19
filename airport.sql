-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Jul 19, 2018 at 09:01 PM
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
(9, 'New Seattle', 'WA'),
(10, 'new', 'WA');

-- --------------------------------------------------------

--
-- Table structure for table `cities_flights`
--

CREATE TABLE `cities_flights` (
  `id` int(11) NOT NULL,
  `city_id` int(11) DEFAULT NULL,
  `flight_number` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `cities_flights`
--

INSERT INTO `cities_flights` (`id`, `city_id`, `flight_number`) VALUES
(1, 1, 100),
(2, 1, 101),
(7, 1, 400),
(8, 2, 200),
(9, 2, 100),
(10, 1, 500),
(11, 2, 600),
(12, 4, 607),
(13, 3, 200);

-- --------------------------------------------------------

--
-- Table structure for table `flights`
--

CREATE TABLE `flights` (
  `id` int(11) NOT NULL,
  `flight_number` int(11) DEFAULT NULL,
  `depart_time` varchar(11) DEFAULT NULL,
  `depart_id` int(11) NOT NULL,
  `arrive_id` int(11) DEFAULT NULL,
  `status` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `flights`
--

INSERT INTO `flights` (`id`, `flight_number`, `depart_time`, `depart_id`, `arrive_id`, `status`) VALUES
(23, 100, '1', 1, 1, 'a'),
(25, 101, '1', 1, 1, 'a'),
(32, 400, '1', 1, 1, 'a'),
(33, 200, '1', 1, 1, 'a'),
(35, 500, '1', 1, 1, 'a'),
(36, 600, '1', 2, 1, 'good to go!'),
(37, 607, '4:00pm', 4, 5, 'open'),
(38, 200, '8:00pm', 3, 3, 'open');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cities`
--
ALTER TABLE `cities`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cities_flights`
--
ALTER TABLE `cities_flights`
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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `cities_flights`
--
ALTER TABLE `cities_flights`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
--
-- AUTO_INCREMENT for table `flights`
--
ALTER TABLE `flights`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
