#include "extcode.h"
#pragma pack(push)
#pragma pack(1)

#ifdef __cplusplus
extern "C" {
#endif
typedef struct {
	LVRefNum UDP;
	LVRefNum TCP;
} ConsCluster;
typedef uint16_t  Val_type_t;
#define Val_type_t_bool_t 0
#define Val_type_t_i32_t 1
#define Val_type_t_u32_t 2
#define Val_type_t_f32_t 3
typedef struct {
	LStrHandle names;
	Val_type_t val_type;
	uint32_t Rawdata;
	int32_t i32;
	uint32_t u32;
	float f32;
	LVBoolean Bool;
} ExVarTree;
typedef struct {
	int32_t dimSize;
	ExVarTree VarTree[1];
} ExVarTreeArrayBase;
typedef ExVarTreeArrayBase **ExVarTreeArray;
typedef struct {
	HWAVEFORM Time;
	LVBoolean CommOK;
} DataCluster;
typedef struct {
	LStrHandle addr;
	uint16_t MainPort;
	uint16_t UDPPort;
} Conns_Params;

/*!
 * Close the Connections
 */
void __stdcall Close_conn(ConsCluster *ConnsID);
/*!
 * Get data from the device.
 */
void __stdcall GetData(ConsCluster *ConnsID, int32_t InputsRate, 
	ExVarTreeArray *VarTree_in, DataCluster *DataStats, 
	ExVarTreeArray *VarTree_out);
/*!
 * Initialize the VarTree
 */
int32_t __stdcall Init(char Filename_Config[], ExVarTreeArray *VarTree);
/*!
 * Connect to the XWave device.
 */
void __stdcall Connect(Conns_Params *XwaveConnection, ConsCluster *ConnsID, 
	LVBoolean *Connected);

MgErr __cdecl LVDLLStatus(char *errStr, int errStrLen, void *module);

/*
* Memory Allocation/Resize/Deallocation APIs for type 'ExVarTreeArray'
*/
ExVarTreeArray __cdecl AllocateExVarTreeArray (int32 elmtCount);
MgErr __cdecl ResizeExVarTreeArray (ExVarTreeArray *hdlPtr, int32 elmtCount);
MgErr __cdecl DeAllocateExVarTreeArray (ExVarTreeArray *hdlPtr);

#ifdef __cplusplus
} // extern "C"
#endif

#pragma pack(pop)

