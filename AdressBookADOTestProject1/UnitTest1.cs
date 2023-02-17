using AddressBookSystem_ADO.Net;

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
        //[TestClass]
        //public class UnitTest1
        //{

        //Usecase 2:Ability to insert new Contacts to Address Book
        [TestMethod]
        public void TestMethodInsertIntoTable()
        {
            int expected = 1;
            ContactDataManager addressBook = new ContactDataManager();
            // AddressBookRespitory addressBookRepository = new AddressBookRespitory();
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
            //var result = addressBookRepository.InsertIntoTable(addressBook);
            //Assert.IsTrue(result);
            int actual = addressBookRepository.InsertIntoTable(addressBook);
            Assert.AreEqual(expected, actual);
        }
    }
}
