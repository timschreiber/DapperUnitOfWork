/*  Written by Tim Schreiber
    StackOverflow user 'sakir' is incorrectly claiming that they wrote this code in the following answer: 
        http://stackoverflow.com/questions/31298235/dapper-and-unit-of-work-pattern/31636037
    
    They have never in any way contributed to this code, and the false attribution has been reported to StackOverflow. */

﻿namespace DapperUnitOfWork.Entities
{
    public class Cat
    {
        public int CatId { get; set; }
        public int BreedId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
