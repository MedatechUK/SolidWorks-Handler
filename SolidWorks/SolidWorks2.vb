﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System.Xml.Serialization

'
'This source code was auto-generated by xsd, Version=4.6.1055.0.
'

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True),
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)>
Partial Public Class Root

    Private rowField() As RootRow

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Row")>
    Public Property Row() As RootRow()
        Get
            Return Me.rowField
        End Get
        Set
            Me.rowField = Value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0"),
 System.SerializableAttribute(),
 System.Diagnostics.DebuggerStepThroughAttribute(),
 System.ComponentModel.DesignerCategoryAttribute("code"),
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)>
Partial Public Class RootRow

    Private idField As String

    Private dESCRIPTIONField As String

    Private mANUFACTURERField As String

    Private pART_NUMBERField As String

    Private iNTERNAL_CODEField As String

    Private qUANTITYField As Double

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="integer")>
    Public Property ID() As String
        Get
            Return Me.idField
        End Get
        Set
            Me.idField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property DESCRIPTION() As String
        Get
            Return Me.dESCRIPTIONField
        End Get
        Set
            Me.dESCRIPTIONField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property MANUFACTURER() As String
        Get
            Return Me.mANUFACTURERField
        End Get
        Set
            Me.mANUFACTURERField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property PART_NUMBER() As String
        Get
            Return Me.pART_NUMBERField
        End Get
        Set
            Me.pART_NUMBERField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property INTERNAL_CODE() As String
        Get
            Return Me.iNTERNAL_CODEField
        End Get
        Set
            Me.iNTERNAL_CODEField = Value
        End Set
    End Property

    '''<remarks/>
    Public Property QUANTITY() As Double
        Get
            Return Me.qUANTITYField
        End Get
        Set
            Me.qUANTITYField = Value
        End Set
    End Property
End Class
