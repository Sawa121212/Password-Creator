   M S B A M L   `   `   `  ����R  NPasswordCreatorVersion2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:PasswordCreatorVersion2PasswordCreatorVersion2  5   	   c6clr-namespace:PasswordCreatorVersion2.Behaviors.Window(PasswordCreatorVersion2.Behaviors.Window  5   	   (    "PasswordCreatorVersion2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:PasswordCreatorVersion2     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5   	   Cwindow6clr-namespace:PasswordCreatorVersion2.Behaviors.Window  5   	     � Title$  Password Creator��5
   	   $	��450��6"   $	��600��6/   $	��300��6;   $	��550��6K   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   � $��0��6   $��0��6   $��10q�6,    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       $
��Auto �6    �� 5!      $
��Auto �6    �� 5"      $
��Auto �6    �� 5#      $
��Auto �6     � ColumnDefinitions 5&      �� 5'      $
��Auto �6#   �� 5(      $��* �6#   �� 5)      $
��Auto �6#   .+�5,      �� $��0��6   $��0��6%   $��Раскладка:��5-      $��Center=�5.      $��5q�63   �� 50      -   5        $��EnglishUpKeysIsOn��50   B   $��1��6   $��1��6$   $	��A-Z��64   $��Center=�51      $
��Left=�62    d� IsThreeState .  52      �� 54      -   5        $��EnglishDownsKeysIsOn��54   B   $��2��6   $��1��6$   $	��a-z��64   $��Bottom=�55      $
��Left=�62    .  56      �� 58      -   5        $��
NumberIsOn��58   B   $��3��6   $��1��6$   $	��0-9��64   $��Center=�59      $
��Left=�62    .   d� 	IsChecked$
 TrueY�5:   ,   �� 5<      $��4��6   $��0��6%   $1��+Стартовая длина пароля:��5=      $��Center=�5>      $��5q�63   �� 5?      -   5        $��PasswordStartLengthBox��5@      $��4��5?      $��1��6#   $��2��63   $��25��5A      $��5q�6"   $��Center=�5B      $��Center=�61   $��Stretch=�6S   $��1��5C      �� 5E      $��6��6   $��0��6%   $��Длина пароля:��5F      $��Center=�5G      $��5q�63   �� 5H      -   5        $��PasswordLengthBox��5I      $��6��5H      $��1��6#   $��2��63   $��Center=�5J      $��Center=�61   $��Stretch=�6S   $��25��5K      $��5q�6"   $��2��6-   .��66   *  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 5L      @   :PasswordCreatorVersion2.Behaviors.Window.KeyWindowBehavior  5M      .��5N   +   5O      �� 5P      $��7��6   $��0��6&   $��2��66    �� Orientation$ 
Horizontal=�5Q      $��Right=�62   $��
10,0,10,10q�5R      .+�5S      �� -   5        $��GenerationButton��5T      $��Generate��5S       � ToolTip$ Генерировать��5U      $	��100��5V      $��30��6%   $��10q�61   ��5X      ��  �� Path$ Generate�	 �� Mode$	 OneWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=��� 5Y      -   5        $��Закрыть��5Y      $' !Закрыть программу��5Z      $	��100��5[      $��30��6%   $��3q�61   $��Right=�5\      5]      5^      �� 5_      $��8��6   $��0��6%   $��2��65   $��5q�6I   Timer6[   �� 5`      -   5         ��Name$ clocktxtblock��5a      $��8��5`      $��1��6%   $��2��65   
�������$��5q�5b      �� 5d      $��9��6   $��0��6%   $��2��65   $��LeftToRight=�5e      .@�5f      �� -	   5        $��StatusBarInfo��5f   -   $	��100��6    $�� ��6B   5g      5h      5i      5j      