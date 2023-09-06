using Musala.Drones.BuildingBlocks.Extensions;
using Shouldly;

namespace DronesTests;

public class StringExtensionsTests
{
    [Fact]
    public void StringExtensionsTest_ValidMedicationName_Ok()
    {
        "medicineForAsma123-For_Kids".IsValidMedicationName().ShouldBeTrue();
        "medicine-002_AB".IsValidMedicationName().ShouldBeTrue();
    }

    [Fact]
    public void StringExtensionsTest_ValidMedicationName_Fail()
    {
        "Medicine For Coll @&".IsValidMedicationName().ShouldBeFalse();
        "##! = F".IsValidMedicationName().ShouldBeFalse();
    }

    [Fact]
    public void StringExtensionsTest_ValidMedicationCode_Ok()
    {
        "MEDICINE_002".IsValidMedicationCode().ShouldBeTrue();
    }

    [Fact]
    public void stringExtensionsTest_ValidMedicationCode_Fail()
    {
        "Invalid Name".IsValidMedicationCode().ShouldBeFalse();
        "Other-invalid @".IsValidMedicationCode().ShouldBeFalse();
    }
}