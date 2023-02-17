using AddressBookSystem_ADO.Net;
using System.Xml.Linq;

namespace AdressBookADOTestProject1
{
    [TestClass]
    public class AddressBookTesting
    {
        AddressBookRespitory addressBookRepository;
        [TestInitialize]

        public void SetUp()
        {
            addressBookRepository = new AddressBookRespitory();
        }
        //Usecase 2:Ability to insert new Contacts to Address Book
        [TestMethod]
        public void TestMethodInsertIntoTable()
        {
            int expected = 1;
            ContactDataManager addressBook = new ContactDataManager();
            addressBook.FirstName = "Rani";
            addressBook.LastName = "Malvi";
            addressBook.Address = "Baker's Street";
            addressBook.City = "Chennai";
            addressBook.State = "TamilNadu";
            addressBook.zip = 243022;
            addressBook.PhoneNumber = 9842905050;
            addressBook.Email = "rani@gmail.com";
            addressBook.Book_Name = "FriendName";
            addressBook.Contact_Type = "Friends";
            int actual = addressBookRepository.InsertIntoTable(addressBook);
            Assert.AreEqual(expected, actual);
        }
    }
    //UseCase 3: Modify Existing Contact using their name
    [TestMethod]
    public void GivenUpdateQueryReturnOne()
    {
        
        bool result = AddressBookRespitory.EditContact( ContactDataManager contact);
        Assert.IsTrue(result);
    }
    //UseCase 4: Delete person based on Name
    [TestMethod]
    public void DeletePersonBasedonName()
    {
        int expected = 1;
        int actual = addressBookRepository.DeletePersonBasedonName();
        Assert.AreEqual(expected, actual);
    }
    //UseCase 5: Ability to Retrieve Person belonging to a City or State from the Address Book
    [TestMethod]
    public void GivenRetrieveQuery_ReturnString()
    {
        string expected = "Harsha Pramela meena ";
        string actual = addressBookRepository.PrintDataBasedOnCity("Bangalore", "Karnataka");
        Assert.AreEqual(expected, actual);
    }

    //UC 6: Ability to Retrieve Count of Person belonging to a City or State
    [TestMethod]
    public void GivenCountQuery_ReturnString()
    {
        string expected = "2 1 3 1 ";
        string actual = addressBookRepository.PrintCountDataBasedOnCity();
        Assert.AreEqual(expected, actual);
    }
}


