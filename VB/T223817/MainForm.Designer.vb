Namespace T223817
    Partial Public Class MainForm
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
            Me.bindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
            Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.repositoryItemCustomEdit1 = New T223817.RepositoryItemCustomEdit()
            DirectCast(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.bindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.repositoryItemCustomEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' gridControl1
            ' 
            Me.gridControl1.DataSource = Me.bindingSource1
            Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gridControl1.Location = New System.Drawing.Point(0, 0)
            Me.gridControl1.MainView = Me.gridView1
            Me.gridControl1.Name = "gridControl1"
            Me.gridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() { Me.repositoryItemCustomEdit1})
            Me.gridControl1.Size = New System.Drawing.Size(520, 411)
            Me.gridControl1.TabIndex = 0
            Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
            ' 
            ' bindingSource1
            ' 
            Me.bindingSource1.DataSource = GetType(T223817.DataItem)
            ' 
            ' gridView1
            ' 
            Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.colName})
            Me.gridView1.GridControl = Me.gridControl1
            Me.gridView1.Name = "gridView1"
            ' 
            ' colName
            ' 
            Me.colName.ColumnEdit = Me.repositoryItemCustomEdit1
            Me.colName.FieldName = "Count"
            Me.colName.Name = "colName"
            Me.colName.Visible = True
            Me.colName.VisibleIndex = 0
            ' 
            ' repositoryItemCustomEdit1
            ' 
            Me.repositoryItemCustomEdit1.AutoHeight = False
            Me.repositoryItemCustomEdit1.Mask.EditMask = "d"
            Me.repositoryItemCustomEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            Me.repositoryItemCustomEdit1.Name = "repositoryItemCustomEdit1"
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(520, 411)
            Me.Controls.Add(Me.gridControl1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            DirectCast(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.bindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.repositoryItemCustomEdit1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private gridControl1 As DevExpress.XtraGrid.GridControl
        Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
        Private bindingSource1 As System.Windows.Forms.BindingSource
        Private colName As DevExpress.XtraGrid.Columns.GridColumn
        Private WithEvents repositoryItemCustomEdit1 As RepositoryItemCustomEdit
    End Class
End Namespace

