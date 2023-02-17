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
}
