   M S B A M L   `   `   `  ����>  :TPV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TPV.WindowsTPV.Windows  5   	       TPV.Windows.Factura   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TPV.Windows     5   	     � Title$  Factura��5   	   $��248.438��6   $
��Auto��5	   	    � SizeToContent$ WidthAndHeight=�6   .��5
      � -   5         �Name$ 
gridGlobal��5
      .+�5   
   �� -   5         ��Name$ label��5      $��Bar Bruxas e Coruxas��6   $
��Left=�6>   $��	54,10,0,0q�6Y   $	��Top=�6l   �� 5   
   -   5        $ label1��5      $��Rodeiro, Lalín��6    $
��Left=�69   $��	71,41,0,0q�6T   $	��Top=�6g   �� 5   
   -   5        $ lblFecha��5      $��Fecha: ��6"   $
��Left=�64   $��	18,86,0,0q�6O   $	��Top=�6b   �� 5   
   -   5        $ lblHora��5      $��Hora: ��6!   $
��Left=�62   $��
119,86,0,0q�6M   $	��Top=�6a   �� 5   
   -   5        $ label4��5      $
��Item��6    $
��Left=�6/   $��
20,136,0,0q�6J   $	��Top=�6^   �� 5   
   -   5        $ label5��5      $
��Cant��6    $
��Left=�6/   $��100,136,0,0q�6J   $	��Top=�6_   �� 5   
   -   5        $ label6��5      $��Precio��6    $
��Left=�61   $��137,136,0,0q�6L   $	��Top=�6a   �� 5   
   -	   5        $ label7��5      $��Total��6    $
��Left=�60   $��185,136,0,0q�6K   $	��Top=�6`   �� 5   
   -
   5        $ label8��5      $1��+-------------------------------------------��6    $
��Left=�6V   $��4,67,0,0q�6q   $	��Top=�6�   �� 5   
   -   5        $ label9��5      $1��+-------------------------------------------��6    $
��Left=�6V   $��	4,125,0,0q�6q   $	��Top=�6�   �� 5   
   -   5        $ label10��5      $1��+-------------------------------------------��6!   $
��Left=�6W   $��	4,143,0,0q�6r   $	��Top=�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $
��Left=�6%   $��	0,162,0,0q�6@   $	��Top=�6S   $��62��6k   $	��230��6w      AutoGenerateColumns .     
IsReadOnly .  � DataContext 6�   ��  �� Source$ 
LineaVenta��	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 95 6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$ Producto.nombre�6N     5      $
 40 6G    6%   �� $ cantidad�6G     5      $
 47 6N    6%   �� $ Producto.precio�6N     5      $
 40 6J    6%   �� $ precioTotal�6J   5      �� 5   
   -   5        $ label2��5      $
��Base��6    $
��Left=�6/   $��
10,221,0,0q�6J   $	��Top=�6^   �� 5   
   -   5        $ label3��5      $��%Desc��6    $
��Left=�60   $��
62,221,0,0q�6K   $	��Top=�6_   �� 5    
   -   5        $ label13��5       $
��Desc��6!   $
��Left=�60   $��125,221,0,0q�6K   $	��Top=�6`   �� 5!   
   -   5        $ label14��5!      $��Subtotal��6!   $
��Left=�64   $��165,221,0,0q�6O   $	��Top=�6d   �� 5"   
   -   5        $ label15��5"      $1��+-------------------------------------------��6!   $
��Left=�6W   $��
10,229,0,0q�6r   $	��Top=�6�   �� 5'   
   -   5        $ label16��5'      $1��+-------------------------------------------��6!   $
��Left=�6W   $��	9,247,0,0q�6r   $	��Top=�6�   �� 5(   
   -   5        $ lblBase��5(      $��Label��6!   $
��Left=�61   $��
10,237,0,0q�6L   $	��Top=�6`   �� 5)   
   -   5        $ lblPDesc��5)      $��Label��6"   $
��Left=�62   $��
69,237,0,0q�6M   $	��Top=�6a   �� 5*   
   -   5        $ lblDesc��5*      $��Label��6!   $
��Left=�61   $��125,237,0,0q�6L   $	��Top=�6a   �� 5+   
   -   5        $ lblSubtotal��5+      $��Label��6%   $
��Left=�65   $��170,237,0,0q�6P   $	��Top=�6e   5.      5/      