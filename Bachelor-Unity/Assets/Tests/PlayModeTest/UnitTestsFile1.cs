using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UnitTestsFile1
{
    Controller controller = Controller.getInstance();
    DataBase db = DataBase.getInstance();

    public UnitTestsFile1()
    {
        controller.setPath("C:/Users/jacob/Documents/point_cloud_data.json");
        controller.LoadController();
        controller.PointLoader();
    }

    [Test]
    public void ReadingJsonLimitsTestPasses()
    {
        // Testing if limits are set to correct values
        Assert.AreEqual(0, db.getShallowDepth());
        Assert.AreEqual(-2, db.getDeepDepth());
        Assert.AreEqual(0, db.getMinLength());
        Assert.AreEqual(10, db.getMaxLength());
        Assert.AreEqual(-2, db.getMinWidth());
        Assert.AreEqual(5, db.getMaxWidth());
    }

    [Test]
    public void LoadingPointsTestPasses()
    {
        // Test if all pings and points are loaded
        Assert.AreEqual(6, db.getPoints().Count);
        Assert.AreEqual(3, db.getNumberOfPings());
    }

    [UnityTest]
    public IEnumerator NormalTriangulateAmountTrianglesTestPasses()
    {
        db.setTriangulationEnabled(true);
        var gameObject = new GameObject();
        var mesh = gameObject.AddComponent<GenerateMesh>();

        yield return new WaitForSeconds(1f);

        // Test if number of triangles is correct
        Assert.AreEqual(15, db.getTriangles().Count);


    }

    [UnityTest]
    public IEnumerator NormalTriangulateTrianglesTestPasses()
    {
        db.setTriangulationEnabled(true);
        var gameObject = new GameObject();
        var mesh = gameObject.AddComponent<GenerateMesh>();

        yield return new WaitForSeconds(1f);

        // Test if triangulation is correct by comparing sorted triangles with the known sorted triangulation
        List<int> triangles = db.getTriangles();
        triangles.Sort();
        List<int> desiredTriangles = new List<int>() {
            0, 0, 1, 1, 2, 2, 2, 2, 2, 3, 3, 4, 4, 5, 5
        };
        Assert.AreEqual(triangles, desiredTriangles);
    }

    [UnityTest]
    public IEnumerator RemovingBorderEdgeNumberTrianglesTestPasses()
    {
        db.setTriangulationEnabled(true);
        db.setEdgeTrianglesRemovalEnabled(true);
        var gameObject = new GameObject();
        var mesh = gameObject.AddComponent<GenerateMesh>();

        yield return new WaitForSeconds(1f);

        // Test if the correct number of triangles exists after removal
        Assert.AreEqual(12, db.getTriangles().Count);
    }

    [UnityTest]
    public IEnumerator RemovingBorderEdgeTrianglesTestPasses()
    {
        db.setTriangulationEnabled(true);
        db.setEdgeTrianglesRemovalEnabled(true);
        var gameObject = new GameObject();
        var mesh = gameObject.AddComponent<GenerateMesh>();

        yield return new WaitForSeconds(1f);

        // Test if the border edge still exists
        for (int i = 0; i < db.getTriangles().Count; i += 3)
        {
            Assert.AreNotEqual(new int[] { db.getTriangles()[i], db.getTriangles()[i + 1] }, new int[] { 1, 0 });
            Assert.AreNotEqual(new int[] { db.getTriangles()[i + 1], db.getTriangles()[i + 2] }, new int[] { 1, 0 });
            Assert.AreNotEqual(new int[] { db.getTriangles()[i + 2], db.getTriangles()[i] }, new int[] { 1, 0 });
        }
    }



}
