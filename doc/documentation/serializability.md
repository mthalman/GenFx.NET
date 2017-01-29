# Component Serializability
All GenFx components are adorned with a [DataContract](https://msdn.microsoft.com/library/system.runtime.serialization.datacontractattribute(v=vs.110).aspx) attribute, allowing them to be serialized with a [DataContractSerializer](https://msdn.microsoft.com/library/system.runtime.serialization.datacontractserializer(v=vs.110).aspx).

This allows you to save the execution state of the algorithm to be resumed later or to transfer the state to another system.

*Note for developers creating 3rd party components*:
It is recommended that any class that is derived from a GenFx component be properly attributed with DataContract and DataMember attributes to allow others to serialize your components as well.