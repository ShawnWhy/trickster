   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  
MainWindow��5   	    � SizeToContent$ WidthAndHeight=�6   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��19* �6   �� 5      $	��13* �6   .+�5   
   �� -   5         ��Name$ createCharacter��5      $��Create Character��6I   $
��Left=�6d   $��45��6   $��
14,364,0,0q�6�   $	��Top=�6�   $	��130��6�   �� 5   
   -   5         ��Name$ 
selectType��5      $��10,46,345,369q�6'    �� Text$ -- Select Team --��6>   $	��120��6W   $��15��6c   �� 5   
   -   5         ��Name$ newName��5      $
��Left=�6#   $��
10,0,0,327q�6>    �� TextWrapping$
 Wrap=�6R   $��TextBox��6f   $	��120��6u   $��15��6�   $��Bottom=�6�   �� 5   
   -   5         ��Name$ ItemsListBox��5      $��103,-80,110,0q�6(   	 J� RenderTransformOrigin$	 0.5,0.5&�6@   $	��281��6`   $	��Top=�6m   $��1��6�   $	��112��6�   z�5      V� 5      .~�5      �� �� 5      �� 5      T� 5      
 T� X$	
 100Q�6)    T� Y$	 100Q�61   5      5      �� 5   
   -   5        $��Add Item��5      $��14,264,331,125q�6<   $	��130��6T   $��45��6`   �� 5   
   -   5        $��Remove Selected Item��5      $��14,314,335,76q�6K   $	 0.714,0.455&�6b   $	��130��6�   $��45��6�   �� 5   
   -   5        $ narrator��5      $
��Left=�6$   $��160,341,0,0q�6?   $
 Wrap=�6T   $��TextBox��6h   $	��Top=�6w   $	��590��6�   $��83��6�   $��2��6�   �� 5   
   -   5         ��Name$ canvas��5      $��160,26,0,115q�6!   $��2��67   $
��Left=�6K   $	��500��6f   $	��300��6r   
����� �6   5       5!      