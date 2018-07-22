Module S_Instances
    Structure InstancedMap
        Dim OriginalMap As Integer
    End Structure

    'Consts
    Friend Const INSTANCED_MAP_MASK As Integer = 16777216 '1 << 24
    Friend Const MAP_NUMBER_MASK As Integer = INSTANCED_MAP_MASK - 1

    Friend Const MAX_INSTANCED_MAPS As Integer = 100
    Friend Const MAX_CACHED_MAPS As Integer = MAX_MAPS + MAX_INSTANCED_MAPS
    Friend Const INSTANCED_MAP_SUFFIX As String = " (Instanced)"

    Friend InstancedMaps(MAX_INSTANCED_MAPS) As InstancedMap

    Friend Sub ClearInstancedMaps()
        Dim i As Integer

        For i = 1 To MAX_INSTANCED_MAPS
            CacheResources(i + MAX_MAPS)
            InstancedMaps(i).OriginalMap = 0
        Next
    End Sub

    Friend Function FindFreeInstanceMapSlot() As Integer
        Dim i As Integer

        For i = 1 To MAX_INSTANCED_MAPS
            If InstancedMaps(i).OriginalMap = 0 Then
                FindFreeInstanceMapSlot = i
                Exit Function
            End If
        Next

        FindFreeInstanceMapSlot = -1
    End Function

    Friend Function CreateInstance(mapNum as Integer) As Integer
        Dim i As Integer, slot As Integer

        If MapNum <= 0 OrElse MapNum > MAX_MAPS Then
            CreateInstance = -1
            Exit Function
        End If

        slot = FindFreeInstanceMapSlot()

        If slot = -1 Then
            CreateInstance = -1
            Exit Function
        End If

        InstancedMaps(slot).OriginalMap = MapNum

        'Copy Map Data
        Map(slot + MAX_MAPS) = Map(MapNum)

        'Copy Map Item Data

        For i = 1 To MAX_MAP_ITEMS
            MapItem(slot + MAX_MAPS, i) = MapItem(MapNum, i)
        Next

        'Copy Map NPCs
        MapNpc(slot + MAX_MAPS) = MapNpc(MapNum)

        'Re-Cache Resource
        CacheResources(slot + MAX_MAPS)

        If Not (Map(slot + MAX_MAPS).Name = vbNullString) Then Map(slot + MAX_MAPS).Name = Map(slot + MAX_MAPS).Name & INSTANCED_MAP_SUFFIX
        CreateInstance = slot
        Exit Function
    End Function

    Friend Sub DestroyInstancedMap(Slot As Integer)
        Dim x As Integer

        ClearMap(Slot + MAX_MAPS)

        For x = 1 To MAX_MAP_NPCS
            ClearMapNpc(x, Slot + MAX_MAPS)
        Next

        For x = 1 To MAX_MAP_ITEMS
            ClearMapItem(x, Slot + MAX_MAPS)
        Next
        InstancedMaps(Slot).OriginalMap = 0
    End Sub

    Friend Function IsInstancedMap(mapNum as Integer) As Boolean
        IsInstancedMap = MapNum > MAX_MAPS AndAlso MapNum <= MAX_CACHED_MAPS
    End Function

    Friend Function GetInstanceBaseMap(mapNum as Integer) As Integer
        GetInstanceBaseMap = InstancedMaps(MapNum - MAX_MAPS).OriginalMap
    End Function
End Module
