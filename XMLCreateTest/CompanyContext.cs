using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Xml;

public class CompanyContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=xmltest");
    }
}

[Table("employee")]
public class Employee
{
    [Column("employeeid")]
    public int ID { get; set; }
    public string 名前 { get; set; }
    public int 年齢 { get; set; }
    public Address 住所 { get; set; }
    public List<Project> プロジェクト { get; set; }
}

[Table("address")]
public class Address
{
    [Column("id")]
    public int ID { get; set; }
    [Column("employeeid")]
    public int EmployeeID { get; set; }
    public string 通り { get; set; }
    public string 市 { get; set; }
    public string 州 { get; set; }
    public string 郵便番号 { get; set; }
}

[Table("project")]
public class Project
{
    [Column("id")]
    public int ID { get; set; }
    [Column("employeeid")]
    public int EmployeeID { get; set; }
    public string 名前 { get; set; }
    public string 期間 { get; set; }
}
