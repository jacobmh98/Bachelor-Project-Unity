using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UnitTestsFile2
{
    Controller controller = Controller.getInstance();
    DataBase db = DataBase.getInstance();

    public UnitTestsFile2()
    {
        /*
         * Initialize the test file 2 and the controller
         */ 
        controller.setPath("./point_cloud_data_2.json");
        controller.LoadController();
    }

    [Test]
    public void LoadingPointsTestPasses()
    {
        controller.PointLoader();
        // Test if all pings and points are loaded
        Assert.AreEqual(7, db.getPoints().Count);
        Assert.AreEqual(3, db.getNumberOfPings());
    }

    [Test]
    public void ReadingJsonLimitsTestPasses()
    {
        // Testing if limits are set to correct values
        Assert.AreEqual(-3, db.getShallowDepth());
        Assert.AreEqual(-22, db.getDeepDepth());
        Assert.AreEqual(0, db.getMinLength());
        Assert.AreEqual(10, db.getMaxLength());
        Assert.AreEqual(-2, db.getMinWidth());
        Assert.AreEqual(5, db.getMaxWidth());
    }

    [Test]
    public void RemovingPointByChangingDepthFilteringTestPasses()
    {
        // Simulate setting a slider value shallow depth of -10
        db.setSliderValueShallowDepth(10);
        // Reload the points
        controller.PointLoader();
        // Test if a single point has been removed
        Assert.AreEqual(6, db.getPoints().Count);
    }

    [Test]
    public void TestPointByChangingDepthFilteringTestPasses()
    {
        // Test if G was the point that was point removed
        foreach (Vector3 p in db.getPoints()) {
            Assert.AreNotEqual(new float[] { p.x, p.y, p.z }, new float[] { 5, -3, 3 });
        }
    }
}
