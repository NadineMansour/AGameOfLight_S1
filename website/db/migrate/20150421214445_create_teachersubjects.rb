class CreateTeachersubjects < ActiveRecord::Migration
  def change
    create_table :teachersubjects do |t|
      t.belongs_to :teacher
      t.belongs_to :subject
      t.boolean :verified , default: false

      t.timestamps null: false
    end
  end
end
