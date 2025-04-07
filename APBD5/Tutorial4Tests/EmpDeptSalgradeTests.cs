using Tutorial3.Models;

public class EmpDeptSalgradeTests
{
    // 1. Simple WHERE filter
    // SQL: SELECT * FROM Emp WHERE Job = 'SALESMAN';
    [Fact]
    public void ShouldReturnAllSalesmen()
    {
        var emps = Database.GetEmps();

        List<Emp> result = emps.Where(e => e.Job == "SALESMAN").ToList();
        ;

        Assert.Equal(2, result.Count);
        Assert.All(result, e => Assert.Equal("SALESMAN", e.Job));
    }

    // 2. WHERE + OrderBy
    // SQL: SELECT * FROM Emp WHERE DeptNo = 30 ORDER BY Sal DESC;
    [Fact]
    public void ShouldReturnDept30EmpsOrderedBySalaryDesc()
    {
        var emps = Database.GetEmps();

        List<Emp> result = emps.Where(e => e.DeptNo == 30).OrderByDescending(s => s.Sal).ToList();

        Assert.Equal(2, result.Count);
        Assert.True(result[0].Sal >= result[1].Sal);
    }

    // 3. Subquery using LINQ (IN clause)
    // SQL: SELECT * FROM Emp WHERE DeptNo IN (SELECT DeptNo FROM Dept WHERE Loc = 'CHICAGO');
    [Fact]
    public void ShouldReturnEmployeesFromChicago()
    {
        var emps = Database.GetEmps();
        var depts = Database.GetDepts();

        var subquery = depts.Where(l => l.Loc == "CHICAGO").Select(d => d.DeptNo).ToList();

        var result = emps.Where(e => subquery.Contains(e.DeptNo)).ToList();

        Assert.All(result, e => Assert.Equal(30, e.DeptNo));
    }

    // 4. SELECT projection
    // SQL: SELECT EName, Sal FROM Emp;
    [Fact]
    public void ShouldSelectNamesAndSalaries()
    {
        var emps = Database.GetEmps();

        var result = emps.Select(e => new { EName = e.EName, Sal = e.Sal }).ToList();

        Assert.All(result, r =>
        {
            Assert.False(string.IsNullOrWhiteSpace(r.EName));
            Assert.True(r.Sal > 0);
        });
    }

    // 5. JOIN Emp to Dept
    // SQL: SELECT E.EName, D.DName FROM Emp E JOIN Dept D ON E.DeptNo = D.DeptNo;
    [Fact]
    public void ShouldJoinEmployeesWithDepartments()
    {
        var emps = Database.GetEmps();
        var depts = Database.GetDepts();

        var result = emps.Join(
            depts,
            e => e.DeptNo,
            d => d.DeptNo,
            (e, d) => new { EName = e.EName, DName = d.DName }).ToList();

        Assert.Contains(result, r => r.DName == "SALES" && r.EName == "ALLEN");
    }

    // 6. Group by DeptNo
    // SQL: SELECT DeptNo, COUNT(*) FROM Emp GROUP BY DeptNo;
    [Fact]
    public void ShouldCountEmployeesPerDepartment()
    {
        var emps = Database.GetEmps();

        var result = emps.GroupBy(d => d.DeptNo)
            .Select(d => new { DeptNo = d.Key, Count = d.Count() }).ToList();

        Assert.Contains(result, g => g.DeptNo == 30 && g.Count == 2);
    }

    // 7. SelectMany (simulate flattening)
    // SQL: SELECT EName, Comm FROM Emp WHERE Comm IS NOT NULL;
    [Fact]
    public void ShouldReturnEmployeesWithCommission()
    {
        var emps = Database.GetEmps();

        var result = emps.Where(c => c.Comm != null).Select(e => new { EName = e.EName, Comm = e.Comm });

        Assert.All(result, r => Assert.NotNull(r.Comm));
    }

    // 8. Join with Salgrade
    // SQL: SELECT E.EName, S.Grade FROM Emp E JOIN Salgrade S ON E.Sal BETWEEN S.Losal AND S.Hisal;
    [Fact]
    public void ShouldMatchEmployeeToSalaryGrade()
    {
        var emps = Database.GetEmps();
        var grades = Database.GetSalgrades();
        var result = from e in emps
            from s in grades
            where e.Sal >= s.Losal && e.Sal <= s.Hisal
            select new { e.EName, s.Grade };

        Assert.Contains(result, r => r.EName == "ALLEN" && r.Grade == 3);
    }

    // 9. Aggregation (AVG)
    // SQL: SELECT DeptNo, AVG(Sal) FROM Emp GROUP BY DeptNo;
    [Fact]
    public void ShouldCalculateAverageSalaryPerDept()
    {
        var emps = Database.GetEmps();

        var result = emps.GroupBy(d => d.DeptNo).Select(d => new { DeptNo = d.Key, AvgSal = d.Average(s => s.Sal) });

        Assert.Contains(result, r => r.DeptNo == 30 && r.AvgSal > 1000);
    }

    // 10. Complex filter with subquery and join
    // SQL: SELECT E.EName FROM Emp E WHERE E.Sal > (SELECT AVG(Sal) FROM Emp WHERE DeptNo = E.DeptNo);
    [Fact]
    public void ShouldReturnEmployeesEarningMoreThanDeptAverage()
    {
        var emps = Database.GetEmps();
        var result = emps
            .Where(e => e.Sal > emps
                .Where(inner => inner.DeptNo == e.DeptNo)
                .Average(inner => inner.Sal))
            .Select(e => e.EName)
            .ToList();

        Assert.Contains("ALLEN", result);
    }
}