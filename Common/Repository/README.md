# Home Manager - Common - Repository

Home Manager uses the Repository pattern to access data.

The default implementation in HomeRepository.cs uses basic SQL to access the data in the Timescale database and Dapper to map SQL data to POCOs.

The repository API is fully async.
