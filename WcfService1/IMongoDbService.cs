using System.Runtime.Serialization;
using System.ServiceModel;
using MongoDB.Bson;

namespace WcfService1
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 인터페이스 이름  "IMongoDbService"을 변경할 수 있습니다.
    [ServiceContract]
    public interface IMongoDbService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        string SampleMethod(string name);

        [OperationContract]
        dynamic Find(ObjectId id);

        [OperationContract]
        bool Save(ObjectId id, dynamic item);

        [OperationContract]
        dynamic Update(ObjectId id, dynamic newValue);

        [OperationContract]
        ObjectId Insert(dynamic item);

        [OperationContract]
        bool SetDatabase(string database, string collection);

        // TODO: 여기에 서비스 작업을 추가합니다.
    }


    // 아래 샘플에 나타낸 것처럼 데이터 계약을 사용하여 복합 형식을 서비스 작업에 추가합니다.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
