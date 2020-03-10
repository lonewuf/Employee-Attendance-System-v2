Imports Microsoft.VisualBasic
Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class Attendance
    Dim DataTableAttendance As New DataTable
    Dim MongoDB As MongoDBConnection = New MongoDBConnection()
    Dim client As MongoClient = MongoDB.client
    Dim db As IMongoDatabase = MongoDB.db
    Dim collection As IMongoCollection(Of BsonDocument) = MongoDB.EmployeeCollection

    Dim xSub As New SampleCallEveryXMinute(3000) ' 1000 ms = 1 sec so 60000 ms = 1 min

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        xSub.StopTimer()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Login.Close()

        DataTableAttendance.Columns.Add("ID", GetType(String))
        DataTableAttendance.Columns.Add("Name", GetType(String))
        DataTableAttendance.Columns.Add("Age", GetType(String))
        DataTableAttendance.Columns.Add("Employee Number", GetType(String))

        Dim filter = Builders(Of BsonDocument).Filter.Empty
        For Each item As BsonDocument In collection.Find(filter).ToList
            Dim str As String
            Dim strArr As Array
            str = item.GetElement("_id").ToString
            strArr = str.Split("=")
            Dim val1 = strArr(1)
            str = item.GetElement("name").ToString
            strArr = str.Split("=")
            Dim val2 = strArr(1)
            str = item.GetElement("age").ToString
            strArr = str.Split("=")
            Dim val3 = strArr(1)
            str = item.GetElement("employee_number").ToString
            strArr = str.Split("=")
            Dim val4 = strArr(1)
            DataTableAttendance.Rows.Add(val1, val2, val3, val4)
        Next

        DGV2.DataSource = DataTableAttendance

        'xSub.StartTimer()
    End Sub


    Private Sub AddNewEmployeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewEmployeeToolStripMenuItem.Click
        RegisterEmployee.Show()
    End Sub

    Private Sub StartFaceRecognitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartFaceRecognitionToolStripMenuItem.Click
        Shell("CMD.exe /k activate base && cd C:\Users\Sampogi\Desktop\vbnet\Employee-Attendance-System-v2\Engine  && python p1_facerecog.py")
    End Sub

    Private Sub TrainFaceRecognitionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TrainFaceRecognitionToolStripMenuItem.Click
        Shell("CMD.exe /k activate base && cd C:\Users\Sampogi\Desktop\vbnet\Employee-Attendance-System-v2\Engine  && python data_preprocess.py && python train_main.py")
    End Sub

    Private Sub AddNewAdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewAdminToolStripMenuItem.Click
        RegisterAdmin.Show()
    End Sub
End Class

Public Class SampleCallEveryXMinute
    Private WithEvents xTimer As New System.Windows.Forms.Timer
    Public count As Int32 = 0
    Public Sub New(TickValue As Integer)
        xTimer = New System.Windows.Forms.Timer
        xTimer.Interval = TickValue
    End Sub

    Public Sub StartTimer()
        xTimer.Start()
    End Sub

    Public Sub StopTimer()
        xTimer.Stop()
    End Sub

    Private Sub Timer_Tick() Handles xTimer.Tick
        SampleProcedure()
    End Sub

    Private Sub SampleProcedure()
        count += 1
        MsgBox("sad " & count)
    End Sub
End Class
