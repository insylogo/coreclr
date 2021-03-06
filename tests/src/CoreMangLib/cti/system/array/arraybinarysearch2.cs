using System;
using System.Globalization;
using System.Collections;
/// <summary>
/// BinarySearch(System.Array,System.Object)
/// </summary>
public class ArrayBinarySearch2 
{
    const int c_MaxValue = 10;
    const int c_MinValue = 0;
    public static int Main()
    {
        ArrayBinarySearch2 ArrayBinarySearch2 = new ArrayBinarySearch2();

        TestLibrary.TestFramework.BeginTestCase("ArrayBinarySearch2");
        if (ArrayBinarySearch2.RunTests())
        {
            TestLibrary.TestFramework.EndTestCase();
            TestLibrary.TestFramework.LogInformation("PASS");
            return 100;
        }
        else
        {
            TestLibrary.TestFramework.EndTestCase();
            TestLibrary.TestFramework.LogInformation("FAIL");
            return 0;
        }
    }

    public bool RunTests()
    {
        bool retVal = true;

        TestLibrary.TestFramework.LogInformation("[Positive]");
        retVal = PosTest1() && retVal;
        retVal = PosTest2() && retVal;
        retVal = PosTest3() && retVal;
        TestLibrary.TestFramework.LogInformation("[Negative]");
        retVal = NegTest1() && retVal;
        retVal = NegTest2() && retVal;
        retVal = NegTest3() && retVal;
       
        return retVal;
    }

    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool PosTest1()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("PosTest1: the Array  contain the specified value which is value type.");

        try
        {

            Array myArray = Array.CreateInstance(typeof(int), c_MaxValue);
            for (int i = myArray.GetLowerBound(0); i <= myArray.GetUpperBound(0); i++)
                myArray.SetValue(i * 2, i);
            int searchValue = (int)myArray.GetValue(c_MaxValue - 1);
            //sort the array
            Array.Sort(myArray);
            int returnvalue = Array.BinarySearch(myArray, searchValue);
            if (returnvalue >= 0)
            {
                if (searchValue != (int)myArray.GetValue(returnvalue))
                {
                    TestLibrary.TestFramework.LogError("001", "BinarySearch falure .");
                    retVal = false;
                }
            }
            else
            {
                TestLibrary.TestFramework.LogError("002", "Postive condition is error.");
                retVal = false;
            }

        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("003", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }
    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool PosTest2()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("PosTest2: the Array  contain the specified value which is reference type.");

        try
        {
            Array myArray = Array.CreateInstance(typeof(string), c_MaxValue);
            string generator = string.Empty;
            for (int i = 0; i < c_MaxValue; i++)
            {
                generator = TestLibrary.Generator.GetString(true, c_MinValue, c_MaxValue);
                myArray.SetValue(generator, i);
            }
            string expectedstring = myArray.GetValue(c_MaxValue - 1).ToString();
            //sort the array
            Array.Sort(myArray, StringComparer.Ordinal);
            string searchValue = expectedstring;
            int returnvalue = Array.BinarySearch(myArray,0, c_MaxValue, searchValue, StringComparer.Ordinal);
            if (returnvalue >= 0)
            {
                if (0 != expectedstring.CompareTo(myArray.GetValue(returnvalue).ToString()))
                {
                    TestLibrary.TestFramework.LogError("004", "BinarySearch falure .");
                    retVal = false;
                }
            }
            else
            {
                TestLibrary.TestFramework.LogError("005", "Postive condition is error.");
                retVal = false;
            }

        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("006", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }

    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool PosTest3()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("PosTest3: the Array  contain the specified value which is user define type.");

        try
        {
            Array myArray = Array.CreateInstance(typeof(Temperature), c_MaxValue);
            Temperature generator = null;
            for (int i = 0; i < c_MaxValue; i++)
            {
                generator = new Temperature();
                generator.Value = i * 4;
                myArray.SetValue(generator, i);
            }
            Temperature expected = myArray.GetValue(c_MaxValue - 1) as Temperature;


            //Temperature searchValue = expectedstring;
            IComparable iComparableImpl = myArray.GetValue(c_MaxValue - 1) as Temperature;
            //sort the array
            Array.Sort(myArray);
            int returnvalue = Array.BinarySearch(myArray, iComparableImpl);
            if (returnvalue >= 0)
            {
                if (!expected.Equals(myArray.GetValue(returnvalue)))
                {
                    TestLibrary.TestFramework.LogError("007", "Search falure .");
                    retVal = false;
                }
            }
            else
            {
                TestLibrary.TestFramework.LogError("008", "Postive condition is error.");
                retVal = false;
            }

        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("009", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }

    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool NegTest1()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("NegTest1: array is a null reference.");

        try
        {
            Array myArray = Array.CreateInstance(typeof(Temperature), c_MaxValue);
            IComparable  iComparableImpl = (Temperature)myArray.GetValue(c_MaxValue - 1);
            myArray = null;
            int returnvalue = Array.BinarySearch(myArray,  iComparableImpl);

            TestLibrary.TestFramework.LogError("010", "array is a null reference.");
            retVal = false;
         }
        catch (ArgumentNullException)
        {
        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("011", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }
    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool NegTest2()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("NegTest2: array is multidimensional.");
        try
        {
            int[] ParamArray ={ c_MaxValue, c_MaxValue };
            Array myArray = Array.CreateInstance(typeof(Temperature), ParamArray);
            int returnvalue = Array.BinarySearch(myArray, c_MaxValue);
            TestLibrary.TestFramework.LogError("012", "array is multidimensional.");
            retVal = false;
        }
        catch (RankException)
        {
        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("013", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }
    
   
    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool NegTest3()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("NegTest3: value does not implement the IComparable interface, and the search encounters an , " +
            "\nelement that does not implement the IComparable interface.");
       
        try
        {
            Array myArray = Array.CreateInstance(typeof(TestClass), c_MaxValue);
            TestClass generator = null;
            for (int i = 0; i < c_MaxValue; i++)
            {
                generator = new TestClass();
                generator.Value = i * 4;
                myArray.SetValue(generator, i);
            }
            //Temperature searchValue = expectedstring;
            TestClass iComparableImpl = myArray.GetValue(c_MaxValue - 1) as TestClass;
            TestClass testValueNotImplTemperature = new TestClass();
            int returnvalue = Array.BinarySearch(myArray, testValueNotImplTemperature);
            TestLibrary.TestFramework.LogError("014", " value does not implement the IComparable interface, and the search encounters an , " +
            "\nelement that does not implement the IComparable interface.");
            retVal = false;
        }
        catch (InvalidOperationException)
        {
        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("015", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }
   
}
//create Temperature  for provding test method and test target.
public class Temperature : IComparable 
{
   
    // The value holder
    protected int m_value;

    public int Value
    {
        get
        {
            return m_value;
        }
        set
        {
            m_value = value;
        }
    }

    public override bool Equals(object obj)
    {
        Temperature temp = (Temperature)obj;
        if (m_value == temp.m_value)
            return true;
        else
            return false;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #region IComparable Members

    public int CompareTo(object obj)
    {
        if (obj is Temperature)
        {
            Temperature temp = (Temperature)obj;

            return m_value.CompareTo(temp.m_value);
        }

        return -1;
    }

    #endregion
}

public class TestClass 
{
    // The value holder
    protected int m_value;

    public int Value
    {
        get
        {
            return m_value;
        }
        set
        {
            m_value = value;
        }
    }
}


