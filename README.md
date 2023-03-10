# ScriptableObjectProtector
ScriptableObjectler için play modunda yapılan değişikliklerin, editor moduna geçerken tekrar eski haline gelemesi için basit bir yardımcı araç sunar.
(ScriptableObject = "SO" kısaltmadır.)

## KULLANIM:
Sadece "ScriptableObject" yerine "ScriptableObjectBase" sınıfından miras alın.
```
public class ExampleSO : ScriptableObjectBase { }
```
## NASIL?:
Miras almayı yaptıktan sonra editorde yeni bir SO objesi oluşturun. Ve En üstte "canResetAfterPlayMode" isminde bir bool değikeni göreceksiniz. Bu değişken "true" olarak işaretliyken PLAY moduna geçerseniz ve SO üzerinde değişiklikler yaparsanız, play modundan çıktıktan sonra SO değerleri eski haline döner (play modunda değişiklik yapılmamış gibi). 

"False" yani işaretsizse o zaman SO'larda yapılan değişiklikler geri alınmaz. Normal olarak kullanılabilir.

## MANTIK:
Mantık basittir. Play Moduna girerken ScriptableObjectBase'den miras alan SO objeler aranır ve JsonUtility ile serileştirilip private değişkende saklanır. Play modundan çıkarken de bu Json metni SO objelere FromJsonOverwrite ile tekrar yazılır.

## SINIRLILIK:
JsonUtility ile kullanıldığı için sadece unity tarafından serileştirilebilir değerlerde çalışıyor. Eğer SO içerisinde private, [System.NonSerialized], DateTime, Dictionary vs gibi unity tarafından serileştirilemez değişkenler kullanıyorsanız, bunlar atlanacaktır (eski haline dönmezler). 

Bunun için geçici çözüm olarak (ExitingPlayMode) dan sonra çalışan ResetSO() isimli override edilebilir bir method tanımladım. Serileştirilemez değerleri veya farklı aksiyonları bu metodu override ederek kullanabilirsiniz.

Not: "ResetSO()" Yalnızca "canResetAfterPlayMode = true" ise çalışır.

### Örnek:
```
  public class ExampleSO : ScriptableObjectBase { 
    protected override void ResetSO()
    {
        base.ResetSO();
        Debug.Log("ExitingPlayMode Run!",this);
    }
  }
```

## NEDEN?:
ScriptableObjectler bir çok farklı alanda, şekilde ve mantıkta kullanılabilen harika şeylerdir. Unity kendi gönderilerinde "ScriptableObjectler değişen veriler için oluşturulmadı" deseler de bunun için kullanan çok kişi var. Ayrıca Unity sitesindeki "ScriptableObject Mimarisi" isimli blogda tam olarak değişen veriler tutan bir SO örneği sunarlar. Orada bu verileri elle eski haline getiriyorlar tabi.

Zaten bu ihtiyaç editor içindir. Hata ayıklamada ve testlerde kullanılabilir. Araştırma yaparsanız buna ihtiyaç duyan insanların sorularından oluşan binlerce gönderi bulabilirsiniz. Alın ve kullanın.

Daha iyi bir yol veya iyileştirme önerilerine açığım.
